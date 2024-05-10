using Messages;
using MongoDB.Driver;
using Hotel.Query.Model;
using Hotel.Query.Repository.ReservationRepository;
using Npgsql.TypeMapping;

namespace Hotel.Query.Repository.HotelInfoRepository
{
    using FreeRoomsInHotel = Dictionary<int, int>;
    using RoomTypesInHotel = Dictionary<int, int>;
    public class RoomTypeInfo
    {
        public String Name { get; set; }
        public int NumberOfPeople { get; set; } 
    }

    public class HotelInfoRepository : IHotelInfoRepository
    {
        const string connectionUri = "mongodb://mongo:27017";
        private IReservationRepository _reservationRepository { get; set; }

        MongoClient _client { get; set; }
        IMongoDatabase _database { get; set; }
        public HotelInfoRepository(IReservationRepository repository) {
            _reservationRepository = repository;
            _client = new MongoClient(connectionUri);
            _database = _client.GetDatabase("hotel_read");
        }


        public async Task<FreeRoomsInHotel> getFreeRoomsInHotel(Model.Hotel hotel)
        {
            var result = new FreeRoomsInHotel();

            foreach (HotelRoomType hrt in hotel.RoomTypes)
            {
                result[hrt.RoomTypeId] = hrt.NumberOfRooms;
            }

            return result;
        }

        public FreeRoomsInHotel createRoomTypesInHotel(Model.Hotel hotel)
        {
            var result = new FreeRoomsInHotel();
            foreach (HotelRoomType hrt in hotel.RoomTypes)
            {
                result[hrt.RoomTypeId] = hrt.NumberOfRooms;
            }

            return result;
        }

        public async Task<Dictionary<int, RoomTypeInfo>> getRoomTypes()
        {
            var roomTypeCollection = _database.GetCollection<RoomType>("room_types");
            var filter = Builders<RoomType>.Filter.Empty;
            var roomTypes = roomTypeCollection.Find(filter).ToList();
            var result = new Dictionary<int, RoomTypeInfo>();
            foreach (var roomType in roomTypes)
            {
                result[roomType.Id] = new RoomTypeInfo() { Name = roomType.Name, NumberOfPeople = roomType.NumberOfPeople };
            }
            return result;
        }

        /*
         Here is defined double dictionary
         Dictionary goes as follows [hotelId: [RoomTypeId: NumberOfRooms]]
         In next part we will subtract reservations from number of rooms
         So that we have only number of free rooms of given room type left
         */
        public async Task<Dictionary<int, FreeRoomsInHotel>> getRoomsInHotelsInCountry(List<Model.Hotel> hotels)
        {
            var result = new Dictionary<int, FreeRoomsInHotel>();
            foreach (var hotel in hotels)
            {
                var roomsInHotel = new FreeRoomsInHotel();
                foreach (HotelRoomType hrt in hotel.RoomTypes)
                {
                    roomsInHotel[hrt.RoomTypeId] = hrt.NumberOfRooms;
                }
                result[hotel.Id] = roomsInHotel;
            }
            return result;
        }

        public async Task<RoomTypesInHotel> getRoomTypesInHotel(Model.Hotel hotel)
        {
            var result = new RoomTypesInHotel();
            foreach (HotelRoomType hrt in hotel.RoomTypes)
            {
                result[hrt.Id] = hrt.RoomTypeId;
            }
            return result;
        }

        public async Task<Dictionary<int, RoomTypesInHotel>> getRoomTypesInHotelsInCountry(List<Model.Hotel> hotels) 
        {
            var result = new Dictionary<int, RoomTypesInHotel>();
            foreach(var hotel in hotels)
            {
                var hotelRoomTypes = new RoomTypesInHotel();
                foreach (HotelRoomType hrt in hotel.RoomTypes)
                {
                    hotelRoomTypes[hrt.Id] = hrt.RoomTypeId;
                }
                result[hotel.Id] = (hotelRoomTypes);
            }
            return result;
        }

        public async Task<HotelQueryResponse> getTripInfo(HotelQuery query)
        {
            var reservationTask = _reservationRepository.GetReservationsByHotelIdAndDate(query.HotelId, query.From, query.To);
            var roomTypesTask = getRoomTypes();
            
            var hotelCollection = _database.GetCollection<Model.Hotel>("hotels");
            var filter = Builders<Model.Hotel>.Filter.Eq<int>(h => h.Id, query.HotelId);
            var hotel = hotelCollection.Find(filter).FirstOrDefault();
            if (hotel == null)
            {
                return null;
            }
            var roomTypeInHotelTask = getRoomTypesInHotel(hotel);
            var hotelRoomsTask = getFreeRoomsInHotel(hotel);

           
            var response = new HotelQueryResponse
            {
                HotelName = hotel.Name,
                City = hotel.City,
                Country = hotel.Country,
                Discount = hotel.Discount,
                FromDate = query.From,
                ToDate = query.To
            };

            List<Messages.Diet> diets = hotel.Diets.Select(d => new Messages.Diet { Id = d.DietId, Name = d.Name }).ToList();
            response.Diets = diets;

            List<Reservation> reservations = await reservationTask;
            var hotelRooms = await hotelRoomsTask;

            var roomTypesInHotel = await roomTypeInHotelTask;
            foreach(Reservation r in reservations)
            {
                foreach(ReservedRoom rr in r.Rooms)
                {
                    hotelRooms[roomTypesInHotel[rr.HotelRoomTypeId]] -= rr.NumberOfRooms;
                }
            }

            var roomTypes = await roomTypesTask;
            response.Rooms = new List<Room>();
            foreach(var room in hotelRooms)
            {
                var roomTypeId = room.Key;
                response.Rooms.Add(new Room()
                {
                    Name = roomTypes[roomTypeId].Name,
                    NumberOfPeople = roomTypes[roomTypeId].NumberOfPeople,
                    NumberOfRooms = room.Value
                });
            }

            return response;

        }

        public async Task<HotelListQueryResponse> getTripListInfo(HotelListQuery query)
        {
            var roomTypeInfoTask = getRoomTypes();

            var hotelCollection = _database.GetCollection<Model.Hotel>("hotels");
            var filter = Builders<Model.Hotel>.Filter.Eq<String>(h => h.Country, query.Country);
            var hotelsInCountry = hotelCollection.Find(filter).ToList();
            var hotelRoomTypesTask = getRoomTypesInHotelsInCountry(hotelsInCountry); 
            var hotelRoomsTask = getRoomsInHotelsInCountry(hotelsInCountry);
            var hotelIds = hotelsInCountry.Select(h => h.Id).ToList();
            var reservationsTask = _reservationRepository.GetReservationsByDatesAndHotelIds(hotelIds, query.From, query.To);

            /*
             * Create task with loading rooms for hotel
             For each hotel create response
             */
            List<HotelQueryResponse> hotels = new List<HotelQueryResponse>();
            foreach(var hotel in hotelsInCountry)
            {
                var response = new HotelQueryResponse()
                {
                    Country = hotel.Country,
                    City = hotel.City,
                    HotelId = hotel.Id,
                    HotelName = hotel.Name,
                    Discount = hotel.Discount,
                    FromDate = query.From,
                    ToDate = query.To,
                    Diets = hotel.Diets.Select(d => new Messages.Diet { Id = d.DietId, Name = d.Name }).ToList()
                };
                hotels.Add(response);
            }

            var reservations = await reservationsTask;
            var roomsInHotels = await hotelRoomsTask;
            var roomTypesForHotels = await hotelRoomTypesTask;

            foreach (Reservation r in reservations)
            {
                var hotelRoomTypes = roomTypesForHotels[r.HotelId];
                foreach(ReservedRoom rr in r.Rooms)
                {
                    roomsInHotels[r.HotelId][hotelRoomTypes[rr.HotelRoomTypeId]] -= rr.NumberOfRooms;
                }
            }

            var roomTypeInfo = await roomTypeInfoTask;
            foreach(var hotel in hotels)
            {
                foreach(var room in roomsInHotels[hotel.HotelId])
                {
                    var roomTypeId = room.Key;
                    var numberOfRooms = room.Value;
                    hotel.Rooms.Add(new Room()
                    {
                        Id = roomTypeId,
                        Name = roomTypeInfo[roomTypeId].Name,
                        NumberOfPeople = roomTypeInfo[roomTypeId].NumberOfPeople,
                        NumberOfRooms = numberOfRooms
                    });
                }
            }

            return new HotelListQueryResponse() { Hotels = hotels };
        }
    }
}

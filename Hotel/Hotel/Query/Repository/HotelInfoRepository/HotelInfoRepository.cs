using Messages;
using MongoDB.Driver;
using Hotel.Query.Model;
using Hotel.Query.Repository.ReservationRepository;

namespace Hotel.Query.Repository.HotelInfoRepository
{
    using RoomTypeDTO = Dictionary<int, int>;
    public class RoomTypeDTODTO
    {
        public String Name { get; set; }
        public int NumberOfPeople { get; set; } 
    }
    
    public class HotelInfoRepository: IHotelInfoRepository
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

        /*
         Here is defined double dictionary
         Dictionary goes as follows [hotelId: [RoomTypeId: NumberOfRooms]]
         In next part we will subtract reservations from number of rooms
         So that we have only number of free rooms of given room type left
         */
        public async Task<RoomTypeDTO> getFreeRoomsInHotel(Model.Hotel hotel)
        {
            var result = new RoomTypeDTO();
            
            foreach (HotelRoomType hrt in hotel.RoomTypes)
            {
                result[hrt.RoomTypeId] = hrt.NumberOfRooms;
            }

            return result;
        }

        public RoomTypeDTO createRoomTypesInHotel(Model.Hotel hotel)
        {
            var result = new RoomTypeDTO();
            foreach (HotelRoomType hrt in hotel.RoomTypes)
            {
                result[hrt.RoomTypeId] = hrt.NumberOfRooms;
            }

            return result;
        }

        public async Task<Dictionary<int, RoomTypeDTODTO>> getRoomTypes()
        {
            var roomTypeCollection = _database.GetCollection<RoomType>("room_types");
            var filter = Builders<RoomType>.Filter.Empty;
            var roomTypes = roomTypeCollection.Find(filter).ToList();
            var result = new Dictionary<int, RoomTypeDTODTO>();
            foreach (var roomType in roomTypes)
            {
                result[roomType.Id] = new RoomTypeDTODTO() { Name = roomType.Name, NumberOfPeople = roomType.NumberOfPeople };
            }
            return result;
        }


        public async Task<HotelQueryResponse> getTripInfo(HotelQuery query)
        {
            /*
             getBasicHotelInfo();
             getNonbasicHotelInfo();
             */
            var reservationTask = _reservationRepository.GetReservationsByHotelIdAndDate(query.HotelId, query.From, query.To);
            var roomTypesTask = getRoomTypes();
            
            var hotelCollection = _database.GetCollection<Model.Hotel>("hotels");
            var filter = Builders<Model.Hotel>.Filter.Eq<int>(h => h.Id, query.HotelId);
            var hotel = hotelCollection.Find(filter).FirstOrDefault();
            var hotelRoomsTask = getFreeRoomsInHotel(hotel);

            if (hotel == null)
            {
                return null;
            }

            var response = new HotelQueryResponse
            {
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

            foreach(Reservation r in reservations)
            {
                foreach(ReservedRoom rr in r.Rooms)
                {
                    hotelRooms[rr.RoomTypeId] -= 1;
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
            throw new NotImplementedException();
        }
    }
}

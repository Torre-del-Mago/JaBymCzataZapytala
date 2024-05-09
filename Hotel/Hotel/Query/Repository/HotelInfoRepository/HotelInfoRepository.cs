using Messages;
using MongoDB.Driver;
using Hotel.Query.Model;
using Hotel.Query.Repository.ReservationRepository;
using Microsoft.VisualBasic;

namespace Hotel.Query.Repository.HotelInfoRepository
{
    public class HotelInfoRepository: IHotelInfoRepository
    {
        const string connectionUri = "mongodb://mongo:27017";
        private IReservationRepository _reservationRepository { get; set; }
        public HotelInfoRepository(IReservationRepository repository) {
            _reservationRepository = repository;
        }


        public async Task<HotelQueryResponse> getTripInfo(HotelQuery query)
        {
            /*
             getBasicHotelInfo();
             getNonbasicHotelInfo();
             */
            var reservationTask = _reservationRepository.GetReservationsByHotelIdAndDate(query.HotelId, query.From, query.To);

            var client = new MongoClient(connectionUri);
            var database = client.GetDatabase("hotel_read");
            var hotelCollection = database.GetCollection<Model.Hotel>("hotels");
            var filter = Builders<Model.Hotel>.Filter.Eq<int>(h => h.Id, query.HotelId);
            var hotel = hotelCollection.Find(filter).FirstOrDefault();

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

            
        }

        /*
         
         */

        public async Task<HotelListQueryResponse> getTripListInfo(HotelListQuery query)
        {
            throw new NotImplementedException();
        }
    }
}

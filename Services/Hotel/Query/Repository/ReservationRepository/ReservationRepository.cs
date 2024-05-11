using Hotel.Query.Model;
using MongoDB.Driver;

namespace Hotel.Query.Repository.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        const string connectionUri = "mongodb://mongo:27017";

        MongoClient _client {get; set;}
        IMongoDatabase _database { get; set;}
        IMongoCollection<Reservation> _reservationCollection { get; set;}

        public ReservationRepository() { 
            _client = new MongoClient(connectionUri);
            _database = _client.GetDatabase("hotel_read");
        }
        public async Task addReservation(Reservation reservation)
        {
            _reservationCollection.InsertOne(reservation);
            return;
        }

        public async Task deleteReservation(int reservationId)
        {
            var filter = Builders<Reservation>.Filter.Eq<int>(r => r.Id, reservationId);
            _reservationCollection.DeleteOne(filter);
            return;
        }

        public async Task<List<Reservation>> GetReservationsByHotelIdAndDate(int HotelId, DateTime fromDate, DateTime toDate)
        {
            var builder = Builders<Reservation>.Filter;
            var filter = builder.Eq<int>(r => r.Id, HotelId) & builder.Lt<DateTime>(r => r.FromDate, fromDate) & builder.Gt<DateTime>(r => r.ToDate, toDate);
            return _reservationCollection.Find(filter).ToList();
        }
    }
}

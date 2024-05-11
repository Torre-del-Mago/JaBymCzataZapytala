using Hotel.DTO;
using Hotel.Query.Repository.ReservationRepository;
using Messages;
using MongoDB.Driver;
using static Hotel.Query.Model.ReservedRoom;

namespace Hotel.Query.Projector
{
    public class HotelEventProjector : IHotelEventProjector
    {
        const string connectionUri = "mongodb://mongo:27017";
        private IReservationRepository _reservationRepository { get; set; }

        MongoClient _client { get; set; }
        IMongoDatabase _database { get; set; }
        public HotelEventProjector(IReservationRepository repository)
        {
            _reservationRepository = repository;
            _client = new MongoClient(connectionUri);
            _database = _client.GetDatabase("hotel_read");
        }
        public async Task projectEvent(ReservationEvent reservationDTO)
        {
            _reservationRepository.addReservation(new Model.Reservation
            {
                Id = reservationDTO.ReservationId,
                HotelId = reservationDTO.HotelId,
                FromDate = reservationDTO.FromDate,
                ToDate = reservationDTO.ToDate,
                Rooms = reservationDTO.RoomsDTO.Select(r => new Model.ReservedRoom
                {
                    HotelRoomTypeId = r.HotelRoomType,
                    Id = r.Id,
                    NumberOfRooms = r.NumberOfRooms
                }).ToList()
            });
        }

        public async Task projectEvent(CanceledReservationEvent canceledReservationDTO)
        {
            _reservationRepository.deleteReservation(canceledReservationDTO.ReservationId);
        }
    }
}

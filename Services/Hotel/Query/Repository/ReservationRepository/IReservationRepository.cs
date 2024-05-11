using Hotel.Query.Model;

namespace Hotel.Query.Repository.ReservationRepository
{
    public interface IReservationRepository
    {
        Task addReservation(Reservation reservation);

        Task deleteReservation(int reservationId);

        Task<List<Reservation>> GetReservationsByHotelIdAndDate(int HotelId, DateTime fromDate, DateTime toDate);
    }
}

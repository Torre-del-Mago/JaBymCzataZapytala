using Hotel.Query.Model;

namespace Hotel.Query.Repository.ReservationRepository
{
    public interface IReservationRepository
    {
        Task addReservation(Reservation reservation);

        Task deleteReservation(int reservationId);

        Task<List<Reservation>> GetReservationsByHotelIdAndDate(int HotelId, DateOnly fromDate, DateOnly toDate);

        Task<List<Reservation>> GetReservationsByDatesAndHotelIds(List<int> hotelIds, DateOnly fromDate, DateOnly toDate);
    }
}

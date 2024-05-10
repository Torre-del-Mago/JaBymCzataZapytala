using Hotel.DTO;

namespace Hotel.Query.Projector
{
    public interface IHotelEventProjector
    {
        Task projectEvent(ReservationDTO reservationDTO);
        Task projectEvent(CanceledReservationDTO canceledReservationDTO);
    }
}

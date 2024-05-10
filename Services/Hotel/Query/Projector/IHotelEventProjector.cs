using Hotel.DTO;

namespace Hotel.Query.Projector
{
    public interface IHotelEventProjector
    {
        Task projectEvent(ReservationEvent reservationDTO);
        Task projectEvent(CanceledReservationEvent canceledReservationDTO);
    }
}

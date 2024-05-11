using Hotel.DTO;

namespace Hotel.Query.Projector
{
    public interface IHotelEventProjector
    {
        void projectEvent(ReservationDTO reservationDTO);
        void projectEvent(CanceledReservationDTO canceledReservationDTO);
    }
}

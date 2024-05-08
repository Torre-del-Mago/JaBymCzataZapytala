using Hotel.DTO;

namespace Hotel.Query.Projector
{
    public class HotelEventProjector : IHotelEventProjector
    {
        public void projectEvent(ReservationDTO reservationDTO)
        {
            throw new NotImplementedException();
        }

        public void projectEvent(CanceledReservationDTO canceledReservationDTO)
        {
            throw new NotImplementedException();
        }
    }
}

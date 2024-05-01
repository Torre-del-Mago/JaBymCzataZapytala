using Hotel.Model.Command;
using Hotel.Model.Event;

namespace Hotel.Repository.CanceledReservation
{
    public class CanceledReservationRepository: ICanceledReservationRepository
    {
        private HotelContext _context;
        public CanceledReservationRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<CanceledReservationEvent> insertEvent(CanceledReservationCommand command)
        {
            CanceledReservationEvent reservationEvent = new CanceledReservationEvent() { 
                ReservationId = command.ReservationId,
            };
            await _context.CanceledReservations.AddAsync(reservationEvent);
            await _context.SaveChangesAsync();
            return reservationEvent;
        }
    }
}

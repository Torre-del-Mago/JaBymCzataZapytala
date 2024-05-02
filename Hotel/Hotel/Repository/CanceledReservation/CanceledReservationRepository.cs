using Hotel.Model.Event;
using Messages;

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
            var active = _context.ActiveReservations.First(x => x.Id == command.ReservationId);
            _context.ActiveReservations.Remove(active);
            await _context.SaveChangesAsync();
            return reservationEvent;
        }
    }
}

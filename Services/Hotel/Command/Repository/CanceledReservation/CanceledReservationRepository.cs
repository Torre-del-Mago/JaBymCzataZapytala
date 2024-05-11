using Hotel.Command.Model;
using Hotel.Command.Repository;
using Messages;

namespace Hotel.Command.Repository.CanceledReservation
{
    public class CanceledReservationRepository : ICanceledReservationRepository
    {
        private HotelContext _context;
        public CanceledReservationRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<CanceledReservationEvent> insertEvent(CanceledReservationCommand command)
        {
            CanceledReservationEvent reservationEvent = new CanceledReservationEvent()
            {
                ReservationId = command.ReservationId,
            };
            await _context.CanceledReservations.AddAsync(reservationEvent);
            await _context.SaveChangesAsync();
            return reservationEvent;
        }
    }
}

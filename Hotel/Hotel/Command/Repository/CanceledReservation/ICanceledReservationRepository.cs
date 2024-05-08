using Messages;
using Hotel.Command.Model;

namespace Hotel.Command.Repository.CanceledReservation
{
    public interface ICanceledReservationRepository
    {
        Task<CanceledReservationEvent> insertEvent(CanceledReservationCommand command);
    }
}

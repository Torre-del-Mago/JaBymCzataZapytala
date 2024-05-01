using Hotel.Model.Command;
using Hotel.Model.Event;

namespace Hotel.Repository.CanceledReservation
{
    public interface ICanceledReservationRepository
    {
        Task<CanceledReservationEvent> insertEvent(CanceledReservationCommand command);
    }
}

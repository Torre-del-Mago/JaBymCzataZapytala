using Hotel.Command.Model.Event;
using Messages;

namespace Hotel.Command.Repository.BookedReservation
{
    public interface IBookedReservationRepository
    {
        Task<bool> canReservationBeMade(BookedReservationCommand command);

        Task<BookedReservationEvent> insertEvent(BookedReservationCommand command);
    }
}

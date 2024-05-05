using Hotel.Command.DTO;
using Messages;

namespace Hotel.Command.Repository.BookedReservation
{
    public interface IBookedReservationRepository
    {
        Task<bool> canReservationBeMade(BookedReservationCommand command);

        Task<BookedEvent> insertEvent(BookedReservationCommand command);
    }
}

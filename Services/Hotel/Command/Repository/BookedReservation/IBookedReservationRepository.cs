using Hotel.Command.Model;
using Hotel.DTO;
using Messages;

namespace Hotel.Command.Repository.BookedReservation
{
    public interface IBookedReservationRepository
    {
        Task<bool> canReservationBeMade(BookedReservationCommand command);

        Task<ReservationEvent> insertEvent(BookedReservationCommand command);
    }
}

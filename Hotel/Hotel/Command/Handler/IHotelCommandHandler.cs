using Messages;

namespace Hotel.Command.CommandHandler
{
    public interface IHotelCommandHandler
    {
        Task HandleCommand(BookedReservationCommand command);
        Task HandleCommand(CanceledReservationCommand command);
    }
}

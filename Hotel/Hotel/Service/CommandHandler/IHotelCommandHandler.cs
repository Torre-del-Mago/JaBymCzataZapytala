using Messages;

namespace Hotel.Service.CommandHandler
{
    public interface IHotelCommandHandler
    {
        Task HandleCommand(BookedReservationCommand command);
        Task HandleCommand(CanceledReservationCommand command);
    }
}

using Messages;

namespace Offer.Service.MessageSender
{
    public interface IMessageSender
    {
        Task SendCanceledTransportCommand(CanceledTransportCommand command);

        Task SendCanceledHotelReservationCommand(CanceledReservationCommand command);

        Task SendReservationFailCommand(ReservationFail command);
    }
}

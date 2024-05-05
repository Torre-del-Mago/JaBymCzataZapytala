using Hotel.Command.Model.Event;
using Messages;

namespace Hotel.Service.MessageSender
{
    public interface IMessageSender
    {
        Task SendNegativeResponseToOffer(BookedReservationCommand command);

        Task SendPositiveResponseToOffer(BookedReservationCommand command, BookedReservationEvent reservationEvent);

        Task SendBookedReservationEvent(BookedReservationEvent reservationEvent, BookedReservationCommand command);

        Task SendCanceledReservationEvent(CanceledReservationCommand command);
    }
}

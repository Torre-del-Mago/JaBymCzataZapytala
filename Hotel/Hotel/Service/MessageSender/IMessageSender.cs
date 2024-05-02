using Hotel.Model.Event;
using Messages;

namespace Hotel.Service.MessageSender
{
    public interface IMessageSender
    {
        Task SendNegativeResponseToOffer(BookedReservationCommand command);

        Task SendPositiveResponseToOffer(BookedReservationCommand command);

        Task SendBookedReservationEvent(BookedReservationEvent reservationEvent, BookedReservationCommand command);

        Task SendCanceledReservationEvent(CanceledReservationCommand command);
    }
}

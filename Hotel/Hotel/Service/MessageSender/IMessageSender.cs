using Hotel.Model.Command;
using Hotel.Model.Event;

namespace Hotel.Service.MessageSender
{
    public interface IMessageSender
    {
        void SendReservationCannotBeMade(BookedReservationCommand command);

        void SendReservationMadeSuccessfully(BookedReservationCommand command);

        void SendBookedReservationEvent(BookedReservationEvent reservationEvent, List<BookedHotelRoomsEvent> hotelRoomsEvent);

        void SendCanceledReservationEvent(CanceledReservationEvent reservationEvent);
    }
}

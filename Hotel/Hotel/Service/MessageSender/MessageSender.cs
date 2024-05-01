using Hotel.Model.Command;
using Hotel.Model.Event;

namespace Hotel.Service.MessageSender
{
    public class MessageSender : IMessageSender
    {
        public void SendBookedReservationEvent(BookedReservationEvent reservationEvent, List<BookedHotelRoomsEvent> hotelRoomsEvent)
        {
            throw new NotImplementedException();
        }

        public void SendReservationCannotBeMade(BookedReservationCommand command)
        {
            throw new NotImplementedException();
        }

        public void SendReservationMadeSuccessfully(BookedReservationCommand command)
        {
            throw new NotImplementedException();
        }


        public void SendCanceledReservationEvent(CanceledReservationEvent reservationEvent)
        {
            throw new NotImplementedException();
        }
    }
}

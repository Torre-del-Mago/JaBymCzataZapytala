using Hotel.Command.Model.Event;

namespace Hotel.Command.DTO
{
    public class BookedEvent
    {
        public BookedReservationEvent BookedReservation { get; set; }
    }
}

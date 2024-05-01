using Hotel.Model.Event;

namespace Hotel.DTO
{
    public class BookedEvent
    {
        public BookedReservationEvent BookedReservation { get; set; }
        public List<BookedHotelRoomsEvent> HotelRooms { get; set;}
    }
}

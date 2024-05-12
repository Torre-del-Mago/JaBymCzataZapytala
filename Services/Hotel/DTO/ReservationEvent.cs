using Hotel.Command.Model;

namespace Hotel.DTO
{
    // Object should be transfered to Query component
    public class ReservationEvent : EventModel
    {
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        //Key is room type id, value is number of rooms 
        public List<BookedHotelRoomsEvent> RoomsDTO { get; set; }
    }
}

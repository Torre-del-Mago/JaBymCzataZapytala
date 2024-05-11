namespace Hotel.DTO
{
    // Object should be transfered to Query component
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //Key is room type id, value is number of rooms 
        public Dictionary<int, int> RoomsDTO { get; set; }
    }
}

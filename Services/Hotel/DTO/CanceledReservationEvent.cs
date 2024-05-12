namespace Hotel.DTO
{
    // Object should be transfered to Query component
    public class CanceledReservationEvent : EventModel
    {
        public int ReservationId { get; set; }
    }
}

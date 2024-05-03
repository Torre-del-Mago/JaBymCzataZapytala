using MassTransit;

namespace Messages
{
    public class BookedReservationCommand: CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }
        public int HotelId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //Key is room type id, value is number of rooms 
        public Dictionary<int, int> RoomsDTO { get; set; }
    }

    public class CanceledReservationCommand
    {
        public int ReservationId { get; set; }
    }

    public class PositiveHotelReservationResponse : CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }

        public int ReservationId { get; set; }
    }

    public class NegativeHotelReservationResponse : CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }
    } 

}

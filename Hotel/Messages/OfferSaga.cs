using MassTransit;

namespace Messages
{
    public class BookedReservationCommand: CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }
        public int HotelId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        //Key is room type id, value is number of rooms 
        public Dictionary<int, int> RoomsDTO { get; set; }
    }

    public class BookedTransportTicketsCommand: CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }
        public int TripId {  get; set; }
        public int NumberOfTickets {  get; set; }
    }

    public class CanceledReservationCommand
    {
        public int ReservationId { get; set; }
    }

    public class CanceledTransportCommand
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

    public class PositiveTransportReservationResponse : CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }

        public int ReservationId { get; set; }
    }

    public class NegativeTransportReservationResponse : CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }
    }

    public class PaidOfferCommand: CorrelatedBy<Guid>
    {
        public string ID { get; set; }
        public Guid CorrelationId { get; set; }
    }
    public class ReservationOfferCommand
    {
        public string ID { get; set; }
        public int HotelId {  get; set; }
        public string Country {  get; set; }
        public string Town { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        /*
         Klucz to roomTypeId,
         Wartość to ilość pokoi tego typu
         */
        public Dictionary<int, int> RoomsDTO { get; set;}
        public string Airport { get; set; }
        public int NumberOfPeople { get; set; }
        public int TripId { get; set; }
    }

    public class ReservationSuccess
    {
        public string ID { get; set; }
    }

    public class ReservationFail
    {
        public string ID { get; set; }
        public string FailReason { get; set; }
    }
}

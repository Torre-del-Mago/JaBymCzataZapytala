using MassTransit;

namespace Offer
{
    class Offer
    {
        public int HotelId { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Dictionary<int, int> RoomsDTO { get; set; }
        public string Airport { get; set; }
        public int NumberOfPeople { get; set; }

        public int HotelReservationId { get; set; }
        public int TransportReservationId { get; set; }
    }
    class OfferReservation : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public int ID { get; set; }

        public Offer Offer { get; set; }

        public bool ReservedHotel { get; set; }

        public int ReservedTransport { get; set; }

    }
    class OfferSaga: MassTransitStateMachine<OfferReservation>
    {
        public State startedReservation { get; set; }
        public State reservedHotel { get; set; }
        public State reservedOffer { get; set; }
        public State paidForOffer { get; set; }
        public State cancelReservations { get; set; }

        public OfferSaga()
        {
            InstanceState(x => x.CurrentState);
        }
    }
}

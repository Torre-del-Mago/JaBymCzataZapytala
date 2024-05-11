using MassTransit;
using Messages;
using Offer.Service.MessageSender;

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

        public int TripId {  get; set; }

        public Offer(ReservationOfferCommand command)
        {
            HotelId = command.HotelId;
            Country = command.Country;
            Town = command.Town;
            startDate = command.startDate;
            endDate = command.endDate;
            RoomsDTO = command.RoomsDTO;
            Airport = command.Airport;
            NumberOfPeople = command.NumberOfPeople;
            TripId = command.TripId;
        }
    }
    class OfferReservation : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string ID { get; set; }

        public Offer Offer { get; set; }

        public int ReservedHotel { get; set; } = 0;

        public int ReservedTransport { get; set; } = 0;

    }
    class OfferSaga: MassTransitStateMachine<OfferReservation>
    {
        public State WaitingForHotel { get; set; }
        public State WaitingForTransport { get; set; }
        public State WaitingForPayment { get; set; }

        private IServiceProvider _serviceProvider;

        public Event<ReservationOfferCommand> StartReservation { get; set; }
        public Event<PositiveHotelReservationResponse> PositiveHotelResponse { get; set; }
        public Event<PositiveTransportReservationResponse> PositiveTransportResponse {  get; set; }
        public Event<NegativeHotelReservationResponse> NegativeHotelResponse { get; set; }
        public Event<NegativeTransportReservationResponse> NegativeTransportResponse { get; set; }
        public Event<PaidOfferCommand> PaidOffer { get; set; }
        
        private void CancelReservations(OfferReservation reservation, string failReason)
        {
            IMessageSender messageSender = _serviceProvider.GetService<IMessageSender>();
            const int NO_RESERVATIONS_MADE = 0;

            if(reservation.ReservedTransport != NO_RESERVATIONS_MADE)
            {
                messageSender.SendCanceledHotelReservationCommand(new CanceledReservationCommand() { ReservationId = reservation.ReservedHotel });
            }
            if(reservation.ReservedTransport != NO_RESERVATIONS_MADE)
            {
                messageSender.SendCanceledTransportCommand(new CanceledTransportCommand() { ReservationId = reservation.ReservedTransport });
            }
            messageSender.SendReservationFailCommand(new ReservationFail() { ID = reservation.ID, FailReason = failReason });
        }

        public OfferSaga(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InstanceState(x => x.CurrentState);

            Event(() => StartReservation, x => x.CorrelateBy(s => s.ID, ctx => ctx.Message.ID).SelectId(context => Guid.NewGuid()));

            Initially(
                When(StartReservation).
                Then(ctx => ctx.Saga.ID = ctx.Message.ID).
                Then(ctx => ctx.Saga.Offer = new Offer(ctx.Message)).
                ThenAsync(ctx => Console.Out.WriteLineAsync($"Order for id {ctx.Message.ID} has begun saga")).
                Publish(ctx => new BookedReservationCommand()
                {
                    ID = ctx.Saga.ID,
                    CorrelationId = ctx.Saga.CorrelationId,
                    HotelId = ctx.Saga.Offer.HotelId,
                    FromDate = ctx.Saga.Offer.startDate,
                    ToDate = ctx.Saga.Offer.endDate,
                    RoomsDTO = ctx.Saga.Offer.RoomsDTO
                }).TransitionTo(WaitingForHotel));

            During(WaitingForHotel,
                When(PositiveHotelResponse).
                Then(ctx => ctx.Saga.ReservedHotel = ctx.Message.ReservationId).
                ThenAsync(ctx => Console.Out.WriteLineAsync($"Reserved Hotel for ID {ctx.Message.ID} with reservation id {ctx.Message.ReservationId}")).
                Publish(ctx => new BookedTransportTicketsCommand()
                {
                    ID = ctx.Saga.ID,
                    CorrelationId = ctx.Saga.CorrelationId,
                    NumberOfTickets = ctx.Saga.Offer.NumberOfPeople,
                    TripId = ctx.Saga.Offer.TripId
                }).TransitionTo(WaitingForTransport),

                When(NegativeHotelResponse).
                ThenAsync(ctx => Console.Out.WriteLineAsync($"Could not reserve hotel for ID {ctx.Message.ID}")).
                Then(ctx => CancelReservations(ctx.Saga, "Could not reserve hotel or transport")).
                Finalize());

            During(WaitingForTransport,
                When(PositiveTransportResponse).
                Then(ctx => ctx.Saga.ReservedTransport = ctx.Message.ReservationId).
                ThenAsync(ctx => Console.Out.WriteLineAsync($"Reserved Transport for ID {ctx.Message.ID} with reservation id {ctx.Message.ReservationId}")).
                Publish(ctx => new ReservationSuccess()
                {
                    ID = ctx.Saga.ID
                }).TransitionTo(WaitingForPayment),

                When(NegativeTransportResponse).
                ThenAsync(ctx => Console.Out.WriteLineAsync($"Could not reserve transport for ID {ctx.Message.ID}")).
                Then(ctx => CancelReservations(ctx.Saga, "Could not reserve hotel or transport")).
                Finalize());

            During(WaitingForPayment,
                When(PaidOffer).
                ThenAsync(ctx => Console.Out.WriteLineAsync($"Paid for offer with ID {ctx.MessageId}")).
                Finalize());

            SetCompletedWhenFinalized();

        }
    }
}

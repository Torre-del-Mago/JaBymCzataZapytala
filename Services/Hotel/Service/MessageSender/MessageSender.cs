using Hotel.Command.Model;
using Hotel.DTO;
using Hotel.Query.Model;
using MassTransit;
using Messages;

namespace Hotel.Service.MessageSender
{
    public class MessageSender : IMessageSender
    {
        private IPublishEndpoint _endpoint;
        private ISendEndpointProvider _provider;
        public MessageSender(IPublishEndpoint endpoint, ISendEndpointProvider provider)
        {
            this._endpoint = endpoint;
            _provider = provider;
        }
        public async Task SendNegativeResponseToOffer(BookedReservationCommand command)
        {
            await _endpoint.Publish<NegativeHotelReservationResponse>(new NegativeHotelReservationResponse() { 
                ID = command.ID,
                CorrelationId = command.CorrelationId
            });
        }

        public async Task SendPositiveResponseToOffer(BookedReservationCommand command, BookedReservationEvent reservationEvent)
        {
            await _endpoint.Publish<PositiveHotelReservationResponse>(new PositiveHotelReservationResponse()
            {
                ID = command.ID,
                CorrelationId = command.CorrelationId,
                ReservationId = reservationEvent.Id
            });
        }

        public async Task SendBookedReservationEvent(ReservationEvent reservation)
        {
            await _provider.GetSendEndpoint(new Uri("queue:hotel-event-queue"));
            await _provider.Send(reservation);
        }

        public async Task SendCanceledReservationEvent(CanceledReservationCommand command)
        {
            await _provider.GetSendEndpoint(new Uri("queue:hotel-event-queue"));
            await _provider.Send<DTO.CanceledReservationEvent>(new
            {
                ReservationId = command.ReservationId
            });
        }

        public Task SendBookedReservationEvent(BookedReservationEvent reservationEvent, BookedReservationCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

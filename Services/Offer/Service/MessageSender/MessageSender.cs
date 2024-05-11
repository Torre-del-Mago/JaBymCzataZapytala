using MassTransit;
using Messages;

namespace Offer.Service.MessageSender
{
    public class MessageSender : IMessageSender
    {
        private IPublishEndpoint _endpoint;
        public MessageSender(IPublishEndpoint endpoint) { 
            this._endpoint = endpoint;
        }

        public Task SendCanceledHotelReservationCommand(CanceledReservationCommand command)
        {
            _endpoint.Publish(command);
            return Task.CompletedTask;
        }

        public Task SendCanceledTransportCommand(CanceledTransportCommand command)
        {
            _endpoint.Publish(command);
            return Task.CompletedTask;
        }

        public Task SendReservationFailCommand(ReservationFail command)
        {
            _endpoint.Publish(command);
            return Task.CompletedTask;
        }
    }
}

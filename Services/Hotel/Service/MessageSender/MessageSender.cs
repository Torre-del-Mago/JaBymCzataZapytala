using Hotel.Command.Model;
using Hotel.DTO;
using MassTransit;
using Messages;

namespace Hotel.Service.MessageSender
{
    public class MessageSender : IMessageSender
    {
        private IPublishEndpoint _endpoint;
        public MessageSender(IPublishEndpoint endpoint) { 
            this._endpoint = endpoint;
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

        public async Task SendBookedReservationEvent(ReservationDTO reservation)
        {
            await _endpoint.Publish<ReservationDTO>(reservation);
        }

        public async Task SendCanceledReservationEvent(CanceledReservationCommand command)
        {
            await _endpoint.Publish<CanceledReservationDTO>(new CanceledReservationDTO()
            {
                ReservationId = command.ReservationId
            });
        }
    }
}

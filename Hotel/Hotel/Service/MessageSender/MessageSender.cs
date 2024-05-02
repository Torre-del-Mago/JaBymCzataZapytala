using Hotel.DTO;
using Hotel.Model.Event;
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

        public async Task SendPositiveResponseToOffer(BookedReservationCommand command)
        {
            await _endpoint.Publish<PositiveHotelReservationResponse>(new PositiveHotelReservationResponse()
            {
                ID = command.ID,
                CorrelationId = command.CorrelationId
            });
        }

        public async Task SendBookedReservationEvent(BookedReservationEvent reservationEvent, BookedReservationCommand command)
        {
            await _endpoint.Publish<BookedReservationForRead>(new BookedReservationForRead()
            {
                ReservationId = reservationEvent.Id,
                FromDate = command.FromDate,
                ToDate = command.ToDate,
                HotelId = command.HotelId,
                RoomsDTO = command.RoomsDTO
            });
        }

        public async Task SendCanceledReservationEvent(CanceledReservationCommand command)
        {
            await _endpoint.Publish<CanceledReservationForRead>(new CanceledReservationForRead()
            {
                ReservationId = command.ReservationId
            });
        }
    }
}

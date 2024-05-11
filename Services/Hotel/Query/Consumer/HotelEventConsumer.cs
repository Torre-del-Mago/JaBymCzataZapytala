using Hotel.DTO;
using Hotel.Query.Projector;
using MassTransit;

namespace Hotel.Query.Consumer
{
    public class HotelEventConsumer : IConsumer<ReservationEvent>, IConsumer<CanceledReservationEvent>
    {
        IHotelEventProjector _projector;
        public HotelEventConsumer(IHotelEventProjector projector) {
            _projector = projector;
        }
        public async Task Consume(ConsumeContext<ReservationEvent> context)
        {
            _projector.projectEvent(context.Message);
        }

        public async Task Consume(ConsumeContext<CanceledReservationEvent> context)
        {
            _projector.projectEvent(context.Message);
        }
    }
}

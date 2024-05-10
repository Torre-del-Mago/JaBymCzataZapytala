using Hotel.Command.CommandHandler;
using MassTransit;
using Messages;

namespace Hotel.Command.Consumer
{
    public class HotelCommandConsumer : IConsumer<BookedReservationCommand>,
        IConsumer<CanceledReservationCommand>
    {
        private IHotelCommandHandler _commandHandler { get; set; }
        public HotelCommandConsumer(IHotelCommandHandler handler)
        {
            _commandHandler = handler;
        }

        public async Task Consume(ConsumeContext<BookedReservationCommand> context)
        {
            await _commandHandler.HandleCommand(context.Message);
        }

        public async Task Consume(ConsumeContext<CanceledReservationCommand> context)
        {
            await _commandHandler.HandleCommand(context.Message);
        }
    }
}

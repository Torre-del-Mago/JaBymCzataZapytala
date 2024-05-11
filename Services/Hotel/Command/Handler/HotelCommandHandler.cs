
using Hotel.Command.Model;
using Hotel.Command.Repository.BookedReservation;
using Hotel.Command.Repository.CanceledReservation;
using Hotel.Service.MessageSender;
using Messages;

namespace Hotel.Command.CommandHandler
{
    public class HotelCommandHandler
        : IHotelCommandHandler
    {
        private IMessageSender _messageSender;
        private IBookedReservationRepository _bookedRepo;
        private ICanceledReservationRepository _canceledRepo;
        public HotelCommandHandler(IMessageSender messageSender, IBookedReservationRepository bookedReservationRepository,
            ICanceledReservationRepository canceledReservationRepository)
        {
            _messageSender = messageSender;
            _bookedRepo = bookedReservationRepository;
            _canceledRepo = canceledReservationRepository;
        }

        public async Task HandleCommand(BookedReservationCommand command)
        {
            var canInsertEvent = await _bookedRepo.canReservationBeMade(command);
            if (!canInsertEvent)
            {
                _messageSender.SendNegativeResponseToOffer(command);
                return;
            }

            var bookedEvent = await _bookedRepo.insertEvent(command);
            // TODO
            //await _messageSender.SendPositiveResponseToOffer(command, bookedEvent);
            //await _messageSender.SendBookedReservationEvent(bookedEvent, command);
        }

        public async Task HandleCommand(CanceledReservationCommand command)
        {
            await _canceledRepo.insertEvent(command);
            await _messageSender.SendCanceledReservationEvent(command);
        }
    }
}

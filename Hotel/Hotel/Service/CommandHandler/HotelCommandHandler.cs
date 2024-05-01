using Hotel.DTO;
using Hotel.Model.Command;
using Hotel.Repository.BookedReservation;
using Hotel.Repository.CanceledReservation;
using Hotel.Service.MessageSender;

namespace Hotel.Service.CommandHandler
{
    public class HotelCommandHandler
        : IHotelCommandHandler
    {
        private IMessageSender _messageSender;
        private IBookedReservationRepository _bookedRepo;
        private ICanceledReservationRepository _canceledRepo;
        public HotelCommandHandler(IMessageSender messageSender, IBookedReservationRepository bookedReservationRepository, 
            ICanceledReservationRepository canceledReservationRepository) {
            _messageSender = messageSender;
            _bookedRepo = bookedReservationRepository;
            _canceledRepo = canceledReservationRepository;
        }

        public async Task HandleCommand(BookedReservationCommand command)
        {
            var canInsertEvent = await _bookedRepo.canReservationBeMade(command);
            if(!canInsertEvent)
            {
                _messageSender.SendReservationCannotBeMade(command);
                return;
            }

            BookedEvent bookedEvent = await _bookedRepo.insertEvent(command);
            _messageSender.SendReservationMadeSuccessfully(command);
            _messageSender.SendBookedReservationEvent(bookedEvent.BookedReservation, bookedEvent.HotelRooms);
        }

        public async Task HandleCommand(CanceledReservationCommand command)
        {
            var canceledReservation = await _canceledRepo.insertEvent(command);
            _messageSender.SendCanceledReservationEvent(canceledReservation);
        }
    }
}

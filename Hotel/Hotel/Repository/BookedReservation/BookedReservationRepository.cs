using Hotel.DTO;
using Hotel.Model.Command;
using Hotel.Model.Event;

namespace Hotel.Repository.BookedReservation
{
    public class BookedReservationRepository : IBookedReservationRepository
    {
        private HotelContext _context;

        public BookedReservationRepository(HotelContext context)
        {
            this._context = context;
        }

        public async Task<bool> canReservationBeMade(BookedReservationCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<BookedEvent> insertEvent(BookedReservationCommand command)
        {
            BookedReservationEvent reservationEvent = new BookedReservationEvent()
            {
                FromDate = command.FromDate,
                ToDate = command.ToDate
            };
            _context.BookedReservations.Add(reservationEvent);

            List<BookedHotelRoomsEvent> hotelRoomsEvents = new List<BookedHotelRoomsEvent>();
            foreach(KeyValuePair<int, int> entry in command.RoomsDTO)
            {
                hotelRoomsEvents.Add(new BookedHotelRoomsEvent()
                {
                    ReservationId = reservationEvent.Id,
                    HotelRoomType = entry.Key,
                    NumberOfRooms = entry.Value
                });
            }
            _context.BookedHotelRooms.AddRange(hotelRoomsEvents);

            await _context.SaveChangesAsync();

            return new BookedEvent() { BookedReservation = reservationEvent, HotelRooms = hotelRoomsEvents };

        }
    }
}

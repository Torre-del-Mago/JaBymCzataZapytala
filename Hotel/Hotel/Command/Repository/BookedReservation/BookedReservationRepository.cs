using Hotel.Command.DTO;
using Hotel.Command.Model.Event;
using Hotel.Command.Repository;
using Messages;

namespace Hotel.Command.Repository.BookedReservation
{
    public class BookedReservationRepository : IBookedReservationRepository
    {
        private HotelContext _context;

        public BookedReservationRepository(HotelContext context)
        {
            _context = context;
        }

        private bool isEnoughRommsAvailable(int typeOfRoom, int numberOfRooms, Dictionary<int, int> roomsTaken, List<CreatedHotelRoomTypeEvent> roomTypes)
        {
            return roomsTaken[typeOfRoom] + numberOfRooms <= roomTypes.First(r => r.RoomTypeId == typeOfRoom).NumberOfRooms;
        }

        public async Task<bool> canReservationBeMade(BookedReservationCommand command)
        {
            List<CreatedHotelRoomTypeEvent> hotelRoomTypes = _context.HotelRoomTypes
                .Where(hotelRoomType => hotelRoomType.HotelId == command.HotelId)
                .ToList();

            List<int> reservationIds = _context.ActiveReservations.Where(r => r.FromDate <= command.FromDate && r.ToDate >= command.ToDate).Select(r => r.Id).ToList();
            List<BookedHotelRoomsEvent> hotelRooms = _context.BookedHotelRooms.Where(hr => reservationIds.Contains(hr.ReservationId)).ToList();
            Dictionary<int, int> hotelTypesTaken = new Dictionary<int, int>();
            foreach (BookedHotelRoomsEvent hr in hotelRooms)
            {
                if (!hotelTypesTaken.ContainsKey(hr.HotelRoomType))
                {
                    hotelTypesTaken.Add(hr.HotelRoomType, hr.NumberOfRooms);
                }
                else
                {
                    hotelTypesTaken[hr.HotelRoomType] += hr.NumberOfRooms;
                }
            }

            foreach (KeyValuePair<int, int> entry in command.RoomsDTO)
            {
                if (!isEnoughRommsAvailable(entry.Key, entry.Value, hotelTypesTaken, hotelRoomTypes))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<BookedEvent> insertEvent(BookedReservationCommand command)
        {
            BookedReservationEvent reservationEvent = new BookedReservationEvent()
            {
                FromDate = command.FromDate,
                ToDate = command.ToDate
            };
            _context.BookedReservations.Add(reservationEvent);
            ActiveBookedReservationEvent activeBooked = new ActiveBookedReservationEvent()
            {
                Id = reservationEvent.Id,
                FromDate = command.FromDate,
                ToDate = command.ToDate
            };
            _context.ActiveReservations.Add(activeBooked);

            List<BookedHotelRoomsEvent> hotelRoomsEvents = new List<BookedHotelRoomsEvent>();
            foreach (KeyValuePair<int, int> entry in command.RoomsDTO)
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

            return new BookedEvent() { BookedReservation = reservationEvent };

        }
    }
}

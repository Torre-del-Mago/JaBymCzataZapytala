using Hotel.Command.Model;
using Hotel.Command.Repository;
using Hotel.DTO;
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

        private bool isEnoughRommsAvailable(int typeOfRoom, int numberOfRooms, Dictionary<int, int> roomsTaken, List<HotelRoomType> roomTypes)
        {
            return roomsTaken[typeOfRoom] + numberOfRooms <= roomTypes.First(r => r.RoomTypeId == typeOfRoom).NumberOfRooms;
        }

        public async Task<bool> canReservationBeMade(BookedReservationCommand command)
        {
            List<HotelRoomType> hotelRoomTypes = _context.HotelRoomTypes
                .Where(hotelRoomType => hotelRoomType.HotelId == command.HotelId)
                .ToList();

            List<int> bookedReservationIds = _context.BookedReservations.Where(r => r.FromDate <= command.FromDate && r.ToDate >= command.ToDate).Select(r => r.Id).ToList();
            List<int> canceledReservationIds = _context.CanceledReservations.Select(r => r.ReservationId).ToList();
            List<int> reservationIds = bookedReservationIds.Where(r => !canceledReservationIds.Contains(r)).ToList();
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

        public async Task<ReservationEvent> insertEvent(BookedReservationCommand command)
        {
            BookedReservationEvent reservationEvent = new BookedReservationEvent()
            {
                FromDate = command.FromDate,
                ToDate = command.ToDate
            };
            _context.BookedReservations.Add(reservationEvent);
            Reservation activeBooked = new Reservation()
            {
                Id = reservationEvent.Id,
                FromDate = command.FromDate,
                ToDate = command.ToDate
            };
            _context.Reservations.Add(activeBooked);

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

            return new ReservationEvent()
            {
                ReservationId = reservationEvent.Id,
                HotelId = reservationEvent.HotelId,
                FromDate = reservationEvent.FromDate, 
                ToDate = reservationEvent.ToDate,
                // RoomsDTO = hotelRoomsEvents
            };

        }
    }
}

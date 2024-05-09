using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Query.Model
{
    public class HotelRoomType
    {
        public int RoomTypeId { get; set; }

        public int NumberOfRooms { get; set; }

        public float PricePerNight { get; set; }
    }
}

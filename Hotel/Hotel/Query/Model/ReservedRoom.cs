using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel.Query.Model
{
    public class ReservedRoom
    {
        public int Id { get; set; }

        public int HotelRoomTypeId { get; set; }
        public int RoomTypeId { get; set; }

        public int NumberOfRooms { get; set; }
    }
}

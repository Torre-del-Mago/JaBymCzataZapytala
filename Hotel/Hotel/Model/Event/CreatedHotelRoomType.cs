using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Model.Event
{
    [Table(name: "created_hotel_room_type")]
    public class CreatedHotelRoomType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("hotel_id")]
        public int HotelId { get; set; }

        [Required]
        [Column("room_type_id")]
        public int RoomTypeId { get; set; }

        [Required]
        [Column("number_of_rooms")]
        public int NumberOfRooms { get; set; }

        [Required]
        [Column("price_per_night")]
        public float PricePerNight { get; set; }
    }
}

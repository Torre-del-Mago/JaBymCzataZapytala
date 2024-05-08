using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model
{
    [Table(name: "booked_hotel_rooms")]
    public class BookedHotelRoomsEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(name: "reservation_id")]
        public int ReservationId { get; set; }

        [Required]
        [Column("hotel_room_type")]
        public int HotelRoomType { get; set; }

        [Required]
        [Column("number_of_rooms")]
        public int NumberOfRooms { get; set; }
    }
}

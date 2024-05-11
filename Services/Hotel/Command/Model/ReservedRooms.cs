using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model
{
    [Table("reserved_rooms")]
    public class ReservedRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("reservation_id")]
        public int ReservationId { get; set; }

        [Required]
        [Column("hotel_room_type")]
        public int HotelRoomTypeId { get; set; }

        [Required]
        [Column("number_of_rooms")]
        public int NumberOfRooms { get; set; }

    }
}

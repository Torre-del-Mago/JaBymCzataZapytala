using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model
{
    [Table(name: "booked_reservation")]
    public class BookedReservationEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("hotel_id")]
        public int HotelId { get; set; }

        [Required]
        [Column("from_date")]
        public DateOnly FromDate { get; set; }

        [Required]
        [Column("to_date")]
        public DateOnly ToDate { get; set; }
    }
}

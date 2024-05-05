using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model.Event
{
    [Table(name: "booked_reservation")]
    public class BookedReservationEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("from_date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Column("to_date")]
        public DateTime ToDate { get; set; }
    }
}

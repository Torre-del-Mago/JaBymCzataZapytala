using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model.Event
{
    [Table(name: "canceled_reservation")]
    public class CanceledReservationEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("reservation_id")]
        public int ReservationId { get; set; }

    }
}

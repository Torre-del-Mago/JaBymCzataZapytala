using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model
{
    [Table(name: "reservation")]
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column("from_date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Column("to_date")]
        public DateTime ToDate { get; set; }
    }
}

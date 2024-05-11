using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Command.Model
{
    [Table("hotel")]
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("discount")]
        public float Discount { get; set; }

        [Required]
        [Column("city")]
        public String City { get; set; }

        [Required]
        [Column("country")]
        public String Country { get; set; }
    }
}

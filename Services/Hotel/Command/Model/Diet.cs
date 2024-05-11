using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Command.Model
{
    [Table("diet")]
    public class Diet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Command.Model
{
    [Table("room_type")]
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("number_of_people")]
        public int NumberOfPeople { get; set; }
    }
}

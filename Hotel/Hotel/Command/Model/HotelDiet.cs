using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Command.Model
{
    [Table("hotel_diet")]
    public class HotelDiet
    {
        [Column("hotel_id")]
        public int HotelId { get; set; }

        [Column("diet_id")]
        public int DietId { get; set; }
    }
}

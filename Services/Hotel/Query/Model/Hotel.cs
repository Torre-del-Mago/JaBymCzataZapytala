using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Query.Model
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Discount { get; set; }
        public String City { get; set; }
        public String Country { get; set; }

        public List<HotelDiet> Diets { get; set; }
        public List<HotelRoomType> RoomTypes { get; set; }
    }
}

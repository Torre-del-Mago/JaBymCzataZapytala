using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Query.Model
{
    public class RoomType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfPeople { get; set; }
    }
}

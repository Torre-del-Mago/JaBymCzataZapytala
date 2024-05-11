using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel.Query.Model
{
    public class Reservation
    {
        public int Id { get; set; }

        public int HotelId { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public List<ReservedRoom> Rooms { get; set; }
    }
}

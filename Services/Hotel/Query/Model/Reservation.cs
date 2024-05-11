using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel.Query.Model
{
    public class Reservation
    {
        public int Id { get; set; }

        public int HotelId { get; set; }
        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }

        public List<ReservedRoom> Rooms { get; set; }
    }
}

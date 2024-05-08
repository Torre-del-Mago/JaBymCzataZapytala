using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel.Query.Model
{
    public class Reservation
    {
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("from_date")]
        public DateTime FromDate { get; set; }

        [BsonElement("to_date")]
        public DateTime ToDate { get; set; }
    }
}

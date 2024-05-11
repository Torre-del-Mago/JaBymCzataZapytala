using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Messages
{
    public class HotelQuery
    {
        public int HotelId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfPeople { get; set; }
    }

    public class HotelListQuery
    {
        public String Country { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfPeople { get; set; }
    }

    public class HotelQueryResponse
    {
        public String HotelName {  get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public float Discount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<Room> Rooms {  get; set; }

        public List<Diet> Diets {  get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPeople { get; set; }

        public int NumberOfRooms { get; set; }
    }

    public class Diet
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
    public class HotelListQueryResponse
    {
        public List<HotelQueryResponse> hotels { get; set; }
    }
}
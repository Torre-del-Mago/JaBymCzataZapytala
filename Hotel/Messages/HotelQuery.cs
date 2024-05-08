namespace Messages
{
    public class HotelQuery
    {
        int HotelId { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
        int NumberOfPeople { get; set; }
    }

    public class HotelListQuery
    {
        String Country { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
        int NumberOfPeople { get; set; }

    }
}
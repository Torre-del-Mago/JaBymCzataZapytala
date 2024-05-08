using Messages;

namespace Hotel.Query.Handler
{
    public interface IHotelQueryHandler
    {
        void getTripListInfo(HotelListQuery query);
        void getTripInfo(HotelQuery query);
    }
}

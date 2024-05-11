using Messages;

namespace Hotel.Query.Repository.HotelInfoRepository
{
    public interface IHotelInfoRepository
    {
        Task<HotelQueryResponse> getTripInfo(HotelQuery query);

        Task<HotelListQueryResponse> getTripListInfo(HotelListQuery query);
    }
}

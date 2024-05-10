using Hotel.Query.Handler;
using Hotel.Query.Repository.HotelInfoRepository;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("/hotel/query/[controller]")]
    public class HotelQueryController: ControllerBase
    {
        private IHotelInfoRepository _repo { get; set; }
        public HotelQueryController(IHotelInfoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet(Name = "hotel-query")]
        public async Task<HotelQueryResponse> GetHotelQuery([FromQuery(Name = "HotelId")] int HotelId,
            [FromQuery(Name = "FromDate")] DateOnly fromDate, [FromQuery(Name = "ToDate")] DateOnly toDate,
            [FromQuery(Name = "NumberOfPeople")] int numberOfPeople)
        {
            return await _repo.getTripInfo(new HotelQuery
            {
                HotelId = HotelId,
                From = fromDate,
                To = toDate,
                NumberOfPeople = numberOfPeople
            });
        }
    }
}

using HotelSevice.Application.Pipelines;
using HotelSevice.Application.UseCases.Queries.GetHotelById;
using MediatR;

namespace HotelSevice.Application.UseCases.Commands.ConfirmHotel
{
    public class ConfirmHotel : IRequest, ICacheRemove
    {
        public string[] CacheKeys => new string[] { new GetHotelById(HotelId).CacheKey };

        public string HotelId { get; set; }

        public ConfirmHotel(string hotelId)
        {
            HotelId = hotelId;
        }
    }
}

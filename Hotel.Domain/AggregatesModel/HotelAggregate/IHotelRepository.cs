using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System.Threading.Tasks;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Hotel Add(Hotel hotel);

        void Update(Hotel hotel);

        Task<Hotel> GetAsync(HotelId hotelId);
    }
}

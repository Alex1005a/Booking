using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSevice.Application
{
    public interface ISearchPort
    {
        void Index(Hotel hotel);
        Task IndexAsync(Hotel hotel);
        Task<Hotel> GetByIdAsync(HotelId id);
        Task<IEnumerable<Hotel>> SearchHotelByName(string name, int page);
    }
}

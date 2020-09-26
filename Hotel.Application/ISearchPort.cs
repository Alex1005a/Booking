using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSevice.Application
{
    public interface ISearchPort
    {
        void Index(Hotel hotel);
        Task<Hotel> GetByIdAsync(string id);
        Task<IReadOnlyCollection<Hotel>> SearchHotelByName(string name, int page);
    }
}

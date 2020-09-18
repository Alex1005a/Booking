using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Hotel Add(Hotel hotel);

        void Update(Hotel hotel);

        Task<Hotel> GetAsync(string hotelId);
    }
}

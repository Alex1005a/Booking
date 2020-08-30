using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.AggregatesModel.HotelAggregate
{
    public interface IHotelRepository : IRepository<HotelAggregate>
    {
        HotelAggregate Add(HotelAggregate hotel);

        void Update(HotelAggregate hotel);

        Task<HotelAggregate> GetAsync(string hotelId);
    }
}

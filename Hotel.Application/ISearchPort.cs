using Hotel.Domain.AggregatesModel.HotelAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application
{
    public interface ISearchPort
    {
        void Index(HotelAggregate hotel);
        Task<HotelAggregate> GetByIdAsync(string id);
    }
}

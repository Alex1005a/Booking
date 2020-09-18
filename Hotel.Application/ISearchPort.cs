using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelSevice.Application
{
    public interface ISearchPort
    {
        void Index(Hotel hotel);
        Task<Hotel> GetByIdAsync(string id);
    }
}

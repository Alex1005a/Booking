using Hotel.Domain.AggregatesModel.HotelAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application
{
    public interface ISearchPort
    {
        void Index(HotelAggregate hotel);
    }
}

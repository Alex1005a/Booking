using Hotel.Domain.AggregatesModel.HotelAggregate;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure
{
    public class FakeRepository : IHotelRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public int Add(HotelAggregate hotel)
        {
            throw new NotImplementedException();
        }

        public async Task<HotelAggregate> GetAsync(int hotelId)
        {
            if (hotelId == 1)
            {
                return new HotelAggregate(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                );
            }
            else
            {
                return new HotelAggregate(
                "cc",
                "zzz",
                "+020 111 94546 333",
                new Address(1, "csd", "vre", "gbh", "jum"),
                new HotelOwner(Guid.NewGuid(), "yum", "+020 111 94546 333")
                );
            }
        }

        public void Update(HotelAggregate hotel)
        {
            throw new NotImplementedException();
        }
    }
}

using Hotel.Domain.AggregatesModel.HotelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure
{
    public class FakeRepository : IHotelRepository
    {
        private readonly TestDbContext context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
                                                                           .UseInMemoryDatabase(databaseName: "Test")
                                                                           .Options);
        public IUnitOfWork UnitOfWork => context;

        public string Add(HotelAggregate hotel)
        {
            context.Hotels.Add(hotel);
            context.SaveChanges();
            return hotel.Id;          
        }

        public async Task<HotelAggregate> GetAsync(string hotelId)
        {
            return await context.Hotels.FindAsync(hotelId);
        }

        public void Update(HotelAggregate hotel)
        {            
            context.Hotels.Update(hotel);           
        }
    }
}

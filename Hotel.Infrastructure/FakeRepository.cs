using Hotel.Domain.AggregatesModel.HotelAggregate;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
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

        private readonly List<HotelAggregate> data = new List<HotelAggregate>();


        public IUnitOfWork UnitOfWork => new FakeUnitOfWork();

        public string Add(HotelAggregate hotel)
        {
            string id = Guid.NewGuid().ToString();
            data.Add(SetId(hotel, id));

            return id;
        }

        public async Task<HotelAggregate> GetAsync(string hotelId)
        {
            var findResult = await Task.Run(() => data.FirstOrDefault(v => v.Id == hotelId));
            return findResult;
        }

        public void Update(HotelAggregate hotel)
        {
            var findResult = data.FirstOrDefault(v => v.Id == hotel.Id);
            data.Remove(findResult);
            data.Add(hotel);
        }

        public HotelAggregate SetId(HotelAggregate hotel, string id)
        {
            PropertyInfo propertyInfo = hotel.GetType().GetProperty("Id");
            propertyInfo.SetValue(hotel, Convert.ChangeType(id, propertyInfo.PropertyType), null);
            return hotel;
        }
    }
}

using HotelSevice.Application;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelSevice.Infrastructure
{
    public class ElasticsearchAdapter : ISearchPort
    {
        readonly ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("hotels");
      
        public void Index(Hotel hotel)
        {
            var client = new ElasticClient(settings);
            client.IndexDocument(hotel);
        }

        public async Task<Hotel> GetByIdAsync(string id)
        {
            var client = new ElasticClient(settings);
            var response = await client.GetAsync<Hotel>(id);
            return response.Source;
        }
    }
}

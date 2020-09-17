using Hotel.Application;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure
{
    public class ElasticsearchAdapter : ISearchPort
    {
        readonly ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("hotels");
      
        public void Index(HotelAggregate hotel)
        {
            var client = new ElasticClient(settings);
            client.IndexDocument(hotel);
        }

        public async Task<HotelAggregate> GetByIdAsync(string id)
        {
            var client = new ElasticClient(settings);
            var response = await client.GetAsync<HotelAggregate>(id);
            return response.Source;
        }
    }
}

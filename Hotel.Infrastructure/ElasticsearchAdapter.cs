using HotelSevice.Application;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelSevice.Infrastructure
{
    public class ElasticsearchAdapter : ISearchPort
    {

        readonly ElasticClient client = AddDefaultMappings(new ConnectionSettings(new Uri("http://localhost:9200"))
                                                     .DefaultIndex("hotels"));


        private static ElasticClient AddDefaultMappings(ConnectionSettings settings)
        {
            string indexName = "hotels";
            settings.DefaultMappingFor<HotelElastic>(m => m
                .IdProperty(p => p.Id)
            );

            ElasticClient client = new ElasticClient(settings);

            client.Indices.Create(indexName,
                index => index.Map<HotelElastic>(x => x.AutoMap())
            );

            return client;
        }

        private Hotel SetHotelId(Hotel hotel, string id)
        {
            PropertyInfo propertyInfo = hotel.GetType().GetProperty("Id");
            propertyInfo.SetValue(hotel, Convert.ChangeType(new HotelId(id), propertyInfo.PropertyType), null);
            return hotel;
        }

        public void Index(Hotel hotel)
        {
            client.IndexDocument(HotelElastic.ToHotelMongo(hotel));
        }

        public async Task IndexAsync(Hotel hotel)
        {
            await client.IndexDocumentAsync(HotelElastic.ToHotelMongo(hotel));
        }

        public async Task<Hotel> GetByIdAsync(HotelId id)
        {
            var response = await client.GetAsync<HotelElastic>(id.Value);
            return SetHotelId(response.Source.ToHotel(), response.Id);
        }

        public async Task<IEnumerable<Hotel>> SearchHotelByName(string name, int page)
        {
            int pageSize = 3;
            int levenshteinDistance = 6;

            Func<QueryContainerDescriptor<HotelElastic>, QueryContainer> searchQuery =
                q => q.Match(m => m
                               .Field(f => f.Name)
                               .Query(name)
                                .Fuzziness(Fuzziness.EditDistance(levenshteinDistance))
                             );
           
            var result = await client.SearchAsync<HotelElastic>(descriptor => descriptor
                                .From(page)
                                .Size(pageSize)
                                .Query(searchQuery)
                           );

            return result.Hits.Select(h =>
            {
                return SetHotelId(h.Source.ToHotel(), h.Id);
            });
        }
    }
}

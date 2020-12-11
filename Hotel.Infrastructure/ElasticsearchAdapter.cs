using HotelSevice.Application;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            settings.DefaultMappingFor<Hotel>(m => m
                .Ignore(p => p.DomainEvents)
            );

            ElasticClient client = new ElasticClient(settings);

            client.Indices.Create(indexName,
                index => index.Map<Hotel>(x => x.AutoMap())
            );

            return client;
        }

        private Hotel SetHotelId(Hotel hotel, string id)
        {
            PropertyInfo propertyInfo = hotel.GetType().GetProperty("Id");
            propertyInfo.SetValue(hotel, Convert.ChangeType(id, propertyInfo.PropertyType), null);
            return hotel;
        }

        public void Index(Hotel hotel)
        {
            client.IndexDocument(hotel);
        }

        public async Task IndexAsync(Hotel hotel)
        {
            await client.IndexDocumentAsync(hotel);
        }

        public async Task<Hotel> GetByIdAsync(string id)
        {
            var response = await client.GetAsync<Hotel>(id);
            return SetHotelId(response.Source, response.Id);
        }

        public async Task<IEnumerable<Hotel>> SearchHotelByName(string name, int page)
        {
            int pageSize = 3;
            int levenshteinDistance = 6;

            Func<QueryContainerDescriptor<Hotel>, QueryContainer> searchQuery =
                q => q.Match(m => m
                               .Field(f => f.Name)
                               .Query(name)
                                .Fuzziness(Fuzziness.EditDistance(levenshteinDistance))
                             );
           
            var result = await client.SearchAsync<Hotel>(descriptor => descriptor
                                .From(page)
                                .Size(pageSize)
                                .Query(searchQuery)
                           );

            return result.Hits.Select(h =>
            {
                return SetHotelId(h.Source, h.Id);
            });
        }
    }
}

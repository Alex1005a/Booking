﻿using HotelSevice.Application;
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
        //readonly ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"))
        //    .DefaultIndex("hotels");

        readonly ElasticClient client = new ElasticClient(new ConnectionSettings(new Uri("http://localhost:9200"))
                                                     .DefaultIndex("hotels"));

        public void Index(Hotel hotel)
        {
            client.IndexDocument(hotel);
        }

        public async Task<Hotel> GetByIdAsync(string id)
        {
            var response = await client.GetAsync<Hotel>(id);
            return response.Source;
        }

        public async Task<IReadOnlyCollection<Hotel>> SearchHotelByName(string name, int page)
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

            return result.Documents;
        }
    }
}

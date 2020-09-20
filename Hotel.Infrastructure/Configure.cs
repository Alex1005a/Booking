using HotelSevice.Application;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Infrastructure
{
    public static class Configure
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<Entity<string>>(cm =>
            {
                cm.SetIsRootClass(true);
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            BsonClassMap.RegisterClassMap<Hotel>(cm =>
            {
                cm.AutoMap();                
            });

            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient(typeof(ISearchPort), typeof(ElasticsearchAdapter));
        }
    }
}

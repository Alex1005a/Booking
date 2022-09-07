using HotelSevice.Application;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace HotelSevice.Infrastructure
{
    public static class Configure
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            /*
            BsonClassMap.RegisterClassMap<Entity<string>>(cm =>
            {
                cm.SetIsRootClass(true);
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            */

            BsonClassMap.RegisterClassMap<HotelOwner>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String));
            });

            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient(typeof(ISearchPort), typeof(ElasticsearchAdapter));
        }
    }
}

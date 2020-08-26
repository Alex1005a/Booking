using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System.Runtime.Serialization;

namespace Hotel.Application.UseCases.Queries.GetHotelById
{
    public class GetHotelById : IRequest<HotelAggregate>, IProvideCacheKey
    {
        public string CacheKey => $"Hotel-{Id}";


        public string Id { get; set; }        

        public GetHotelById(string id)
        {
            Id = id;
        }
    }
}

using HotelSevice.Application.Pipelines;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;

namespace HotelSevice.Application.UseCases.Queries.GetHotelById
{
    public class GetHotelById : IRequest<Hotel>, IProvideCacheKey
    {
        public string CacheKey => $"Hotel-{Id}";


        public string Id { get; set; }        

        public GetHotelById(string id)
        {
            Id = id;
        }
    }
}

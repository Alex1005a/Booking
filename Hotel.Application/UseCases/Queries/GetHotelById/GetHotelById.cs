using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System.Runtime.Serialization;

namespace Hotel.Application.UseCases.Queries.GetHotelById
{
    public class GetHotelById : IRequest<HotelAggregate>, IProvideCacheKey
    {
        public string CacheKey => $"Hotel-{Id}";

        [DataMember]
        public int Id { get; private set; }        

        public GetHotelById(int id)
        {
            Id = id;
        }
    }
}

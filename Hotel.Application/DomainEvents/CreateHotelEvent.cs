using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;

namespace HotelSevice.Application.DomainEvents
{
    public class CreateHotelEvent : INotification 
    {
        public Hotel Hotel { get; set; }
    }
}

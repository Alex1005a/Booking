using HotelSevice.Application.DomainEvents;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.DomainEventsHandlers
{
    public class CreateHotelEventHandler : INotificationHandler<CreateHotelEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;     

        public CreateHotelEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;           
        }

        public Task Handle(CreateHotelEvent notification, CancellationToken cancellationToken)
        {            
            _publishEndpoint.Publish(notification);

            return Task.CompletedTask;
        }
    }

}

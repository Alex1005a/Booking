using HotelSevice.Application.DomainEvents;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.DomainEventsHandlers
{
    public class ConfirmHotelEventHandler : INotificationHandler<ConfirmHotelEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ConfirmHotelEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;           
        }

        public Task Handle(ConfirmHotelEvent notification, CancellationToken cancellationToken)
        {            
            _publishEndpoint.Publish(notification);

            return Task.CompletedTask;
        }
    }
}

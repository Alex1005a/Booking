using Hotel.Application.DomainEvents;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.DomainEventsHandlers
{
    public class UpdateHotelStatusEventHandler : INotificationHandler<UpdateHotelStatusEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateHotelStatusEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Handle(UpdateHotelStatusEvent notification, CancellationToken cancellationToken)
        {
            _publishEndpoint.Publish(notification);

            return Task.CompletedTask;
        }
    }
}

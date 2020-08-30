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
        private readonly ISearchPort _searchPort;

        public UpdateHotelStatusEventHandler(IPublishEndpoint publishEndpoint, ISearchPort searchPort)
        {
            _publishEndpoint = publishEndpoint;
            _searchPort = searchPort;
        }

        public Task Handle(UpdateHotelStatusEvent notification, CancellationToken cancellationToken)
        {
            _searchPort.Index(notification.Hotel);

            _publishEndpoint.Publish(notification);

            return Task.CompletedTask;
        }
    }
}

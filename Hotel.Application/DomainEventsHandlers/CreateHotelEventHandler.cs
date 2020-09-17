﻿using Hotel.Application.DomainEvents;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.DomainEventsHandlers
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

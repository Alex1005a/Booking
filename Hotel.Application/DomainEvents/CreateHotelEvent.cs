using Hotel.Application.UseCases.Commands.CreateHotel;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DomainEvents
{
    public class CreateHotelEvent : INotification 
    {
        public HotelAggregate Hotel { get; set; }
    }
}

using HotelSevice.Application.UseCases.Commands.CreateHotel;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.DomainEvents
{
    public class CreateHotelEvent : INotification 
    {
        public Hotel Hotel { get; set; }
    }
}

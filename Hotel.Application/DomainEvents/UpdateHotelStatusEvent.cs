using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DomainEvents
{
    public class UpdateHotelStatusEvent : INotification
    {
        public HotelAggregate Hotel { get; set; }
        public bool Status { get; set; }

        public UpdateHotelStatusEvent(HotelAggregate hotel, bool status)
        {
            Hotel = hotel;
            Status = status;
        }
    }
}

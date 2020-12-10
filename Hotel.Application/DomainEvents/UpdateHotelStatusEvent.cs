using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.DomainEvents
{
    public class UpdateHotelStatusEvent : INotification
    {
        public Hotel Hotel { get; set; }
        public bool Status { get; set; }

        public UpdateHotelStatusEvent(Hotel hotel, bool status)
        {
            Hotel = hotel;
            Status = status;
        }
    }
}

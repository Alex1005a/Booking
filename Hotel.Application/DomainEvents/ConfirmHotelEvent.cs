using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.DomainEvents
{
    public class ConfirmHotelEvent : INotification
    {
        public string HotelId { get; set; }

        public ConfirmHotelEvent(string hotelId)
        {
            HotelId = hotelId;
        }
    }
}

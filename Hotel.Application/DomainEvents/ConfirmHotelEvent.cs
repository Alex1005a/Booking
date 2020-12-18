using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.DomainEvents
{
    public class ConfirmHotelEvent : INotification
    {
        public HotelId HotelId { get; set; }

        public ConfirmHotelEvent(HotelId hotelId)
        {
            HotelId = hotelId;
        }
    }
}

using Hotel.Application.UseCases.Queries.GetHotelById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.UseCases.Commands.UpdateHotelStatus
{
    public class UpdateHotelStatus : IRequest, ICacheRemove
    {
        public string CacheKey => new GetHotelById(HotelId).CacheKey;

        public string HotelId { get; set; }
        public bool Status { get; set; }

        public UpdateHotelStatus(string hotelId, bool status)
        {
            HotelId = hotelId;
            Status = status;
        }
    }
}

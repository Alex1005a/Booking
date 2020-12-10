using HotelSevice.Application.Pipelines;
using HotelSevice.Application.UseCases.Queries.GetHotelById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.UseCases.Commands.UpdateHotelStatus
{
    public class UpdateHotelStatus : IRequest, ICacheRemove
    {
        public string[] CacheKeys => new string[] { new GetHotelById(HotelId).CacheKey };

        public string HotelId { get; set; }
        public bool Status { get; set; }

        public UpdateHotelStatus(string hotelId, bool status)
        {
            HotelId = hotelId;
            Status = status;
        }
    }
}

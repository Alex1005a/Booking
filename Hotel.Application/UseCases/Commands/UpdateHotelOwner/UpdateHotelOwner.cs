using HotelSevice.Application.Pipelines;
using HotelSevice.Application.UseCases.Queries.GetHotelById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.UseCases.Commands.UpdateHotelOwner
{
    public class UpdateHotelOwner : IRequest, ICacheRemove
    {
        public string[] CacheKeys => new string[] { new GetHotelById(HotelId).CacheKey };

        public string HotelId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public UpdateHotelOwner(string hotelId, Guid id, string name, string phoneNumber)
        {
            HotelId = hotelId;
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}

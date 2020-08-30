using Hotel.Application.Pipelines;
using Hotel.Application.UseCases.Queries.GetHotelById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.UseCases.Commands.UpdateHotelOwner
{
    public class UpdateHotelOwner : IRequest, ICacheRemove
    {
        public string CacheKey => new GetHotelById(HotelId).CacheKey;

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

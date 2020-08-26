using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.UseCases.Commands.UpdateHotelOwner
{
    public class UpdateHotelOwner : IRequest
    {
        public string HotelId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public UpdateHotelOwner(string hotelId ,Guid id, string name, string phoneNumber)
        {
            HotelId = hotelId;
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}

using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.UseCases.Commands.CreateHotel
{
    public class CreateHotel : IRequest<string>
    {    
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public HotelOwner HotelOwner { get; set; }
        public Address Address { get; set; }

        public CreateHotel() { }

        public CreateHotel(string name, string description, string phoneNumber, Address address, HotelOwner hotelOwner)
        {
            Name = name;
            Description = description;
            PhoneNumber = phoneNumber;
            Address = address;
            HotelOwner = hotelOwner;
        }
    }
}

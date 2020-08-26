using Hotel.Application.UseCases.Commands.CreateHotel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DomainEvents
{
    public class CreateHotelEvent : INotification 
    {
        public CreateHotel CreateHotel { get; set; }
        public string Id { get; set; }
    }
}

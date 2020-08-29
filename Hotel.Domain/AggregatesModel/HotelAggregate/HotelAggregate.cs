using Hotel.Domain.AggregatesModel.Exeptions;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Hotel.Domain.AggregatesModel.HotelAggregate
{
    public class HotelAggregate : Entity<string>, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Description { get; private set; }
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Not correct a phone number")]
        public string PhoneNumber { get; private set; }

        public bool? Approved { get; private set; }

        public DateTime CreatedTime { get; private set; }

        public Address Address { get; private set; }

        public HotelOwner HotelOwner { get; private set; }

        private HotelAggregate() { }

        public HotelAggregate(string name, string description, string phoneNumber, Address address, HotelOwner hotelOwner) 
        {
            Name = name;
            Description = description;

            Regex phoneNumpattern = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            if (!phoneNumpattern.IsMatch(phoneNumber))
            {
                throw new HotelDomainException($"{phoneNumber} is not a phone number");
            }
            PhoneNumber = phoneNumber;
            Address = address;
            HotelOwner = hotelOwner;

            CreatedTime = DateTime.Now;
        }

        public void UpdateOwner(HotelOwner hotelOwner)
        {
            if(HotelOwner.Id == hotelOwner.Id)
            {
                HotelOwner = hotelOwner;
            }
        }
        public void SetStatus(bool approved)
        {
            Approved = approved;
        }
    }
}

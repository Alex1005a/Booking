using Hotel.Domain.AggregatesModel.Exeptions;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Hotel.Domain.AggregatesModel.HotelAggregate
{
    public class HotelAggregate : Entity<int>, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Description { get; private set; }
        [RegularExpression(@"\+[0-9]{3}\s+[0-9]{3}\s+[0-9]{5}\s+[0-9]{3}", ErrorMessage = "Not correct a phone number")]
        public string PhoneNumber { get; private set; }

        public Address Address { get; private set; }

        public HotelOwner HotelOwner { get; private set; }


        public HotelAggregate(string name, string description, string phoneNumber, Address address, HotelOwner hotelOwner) 
        {
            Name = name;
            Description = description;

            Regex phoneNumpattern = new Regex(@"\+[0-9]{3}\s+[0-9]{3}\s+[0-9]{5}\s+[0-9]{3}");
            if (!phoneNumpattern.IsMatch(phoneNumber))
            {
                throw new HotelDomainException($"{phoneNumber} is not a phone number");
            }
            PhoneNumber = phoneNumber;
            Address = address;
            HotelOwner = hotelOwner;
        }
    }
}

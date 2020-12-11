using HotelSevice.Domain.AggregatesModel.Exeptions;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    public class Hotel : Entity<string>, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public bool? Confirmed { get; private set; }

        public string[] Tags { get; private set; }

        public DateTime CreatedTime { get; private set; }

        public Address Address { get; private set; }

        public HotelOwner HotelOwner { get; private set; }

        private Hotel() { }

        public Hotel(string name, string description, PhoneNumber phoneNumber, Address address, HotelOwner hotelOwner) 
        {
            Name = name;
            Description = description;
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

        public void Confirm()
        {
            Confirmed = true;
        }

        public void Reject()
        {
            Confirmed = false;
        }
    }
}

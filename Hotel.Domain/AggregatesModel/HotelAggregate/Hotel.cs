using HotelSevice.Domain.AggregatesModel.Exeptions;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    public class Hotel : Entity<HotelId>, IAggregateRoot
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

        public Hotel(HotelId id, string name, string description, PhoneNumber phoneNumber, DateTime createdTime, Address address, HotelOwner hotelOwner) 
        {
            Id = id;
            Name = name;
            Description = description;
            PhoneNumber = phoneNumber;
            Address = address;
            HotelOwner = hotelOwner;

            CreatedTime = createdTime;
        }

        public void TryChangeOwner(HotelOwner hotelOwner)
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

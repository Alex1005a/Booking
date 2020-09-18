using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    public class Address : ValueObject
    {
        public int HouseNumber { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        public Address(int houseNumber, string street, string city, string state, string country)
        {
            HouseNumber = houseNumber;
            Street = street;
            City = city;
            State = state;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return HouseNumber;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
        }
    }
}

using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    [Serializable]
    public class HotelOwner : ValueObject
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public HotelOwner(Guid id, string name, string phoneNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;
            yield return PhoneNumber;
        }
    }
}

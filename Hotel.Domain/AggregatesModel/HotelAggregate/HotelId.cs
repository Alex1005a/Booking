using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    public class HotelId : ValueObject
    {
        public string Value { get; private set; }

        public HotelId(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static HotelId Generate()
        {
            return new HotelId(Guid.NewGuid().ToString());
        }
    }
}

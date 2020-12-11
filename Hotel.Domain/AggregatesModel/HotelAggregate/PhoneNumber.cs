using HotelSevice.Domain.AggregatesModel.Exeptions;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HotelSevice.Domain.AggregatesModel.HotelAggregate
{
    [Serializable]
    public class PhoneNumber : ValueObject
    {
        public string Value { get; private set; }

        public PhoneNumber(string value)
        {
            Regex phoneNumpattern = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            if (!phoneNumpattern.IsMatch(value))
            {
                throw new HotelDomainException($"{value} is not a phone number");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}

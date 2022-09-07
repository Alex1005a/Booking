using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    public class UserId : ValueObject
    {
        public string Value { get; private set; }

        public UserId(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static UserId Generate()
        {
            return new UserId(Guid.NewGuid().ToString());
        }
    }
}

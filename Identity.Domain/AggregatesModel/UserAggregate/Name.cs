using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using Monad;
using System.Text.RegularExpressions;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    [Serializable]
    public class Name : ValueObject
    {
        public const int MaxNameLength = 256;
        public string Value { get; private set; }

        private Name(string value)
        {

            Value = value;
        }

        public static Either<NameError, Name> Create(string name)
        {
            if (name.Length > MaxNameLength) return () => NameError.TooLongName;
            Regex notHasSpecialSymbols = new Regex("^[a-zA-Z0-9 ]*$");
            if (!notHasSpecialSymbols.IsMatch(name) || name == "")
            {
                return () => NameError.NotValidName;
            }
            return () => new Name(name);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }

    public enum NameError
    {
        NotValidName,
        TooLongName
    }
}

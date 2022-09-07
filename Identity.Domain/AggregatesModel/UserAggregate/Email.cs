using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using Monad;
using System.Text.RegularExpressions;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    [Serializable]
    public class Email : ValueObject
    {
        public const int MaxEmailLength = 254;
        public string Value { get; private set; }

        private Email(string value)
        {
            
            Value = value;
        }

        public static Either<EmailError, Email> Create(string email)
        {
            if(email.Length > MaxEmailLength) return () => EmailError.TooLongEmail;
            Regex emailNumpattern = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!emailNumpattern.IsMatch(email))
            {
                return () => EmailError.NotValidEmail;
            }
            return () => new Email(email);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }

    public enum EmailError
    {
        NotValidEmail,
        TooLongEmail
    }
}

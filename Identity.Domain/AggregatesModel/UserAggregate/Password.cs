using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using Monad;
using System.Text.RegularExpressions;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    [Serializable]
    public class Password : ValueObject
    {
        public const int MaxPasswordLength = 254;
        public const int MinPasswordLength = 5;
        public string Value { get; private set; }

        private Password(string value)
        {

            Value = value;
        }

        public static Either<PasswordError, Password> Create(string password)
        {
            if (password.Length > MaxPasswordLength) return () => PasswordError.TooLongPassword;
            if(password.Length < MinPasswordLength) return () => PasswordError.TooShortPassword;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            if (!hasNumber.IsMatch(password) || !hasUpperChar.IsMatch(password))
            {
                return () => PasswordError.NotValidPassword;
            }
            return () => new Password(password);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
    public enum PasswordError
    {
        NotValidPassword,
        TooLongPassword,
        TooShortPassword
    }
}

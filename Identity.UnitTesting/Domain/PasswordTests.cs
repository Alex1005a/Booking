using Identity.Domain.AggregatesModel.UserAggregate;
using Monad;
using Xunit;

namespace Identity.UnitTesting.Domain
{
    public class PasswordTests
    {
        [Fact]
        public void Too_long_password_error()
        {
            string longPassword = new string('a', Password.MaxPasswordLength + 1);
            var result = Password.Create(longPassword);
            Assert.True(result.IsLeft() && result.Left() == PasswordError.TooLongPassword);
        }

        [Fact]
        public void Too_short_password_error()
        {
            string shortPassword = "a";
            var result = Password.Create(shortPassword);
            Assert.True(result.IsLeft() && result.Left() == PasswordError.TooShortPassword);
        }

        [Fact]
        public void Not_valid_password_error()
        {
            string fakePassword = "password";
            var result = Password.Create(fakePassword);
            Assert.True(result.IsLeft() && result.Left() == PasswordError.NotValidPassword);
        }

        [Fact]
        public void Valid_password()
        {
            string password = "pASsword23";
            var result = Password.Create(password);
            Assert.True(result.IsRight());
        }
    }
}

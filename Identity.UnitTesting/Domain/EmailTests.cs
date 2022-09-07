using Identity.Domain.AggregatesModel.UserAggregate;
using Monad;
using Xunit;

namespace Identity.UnitTesting.Domain
{
    public class EmailTests
    {
        [Fact]
        public void Too_long_email_error()
        {
            string longEmail = new string('a', Email.MaxEmailLength + 1);
            var result = Email.Create(longEmail);
            Assert.True(result.IsLeft() && result.Left() == EmailError.TooLongEmail);
        }

        [Fact]
        public void Not_valid_email_error()
        {
            string fakeEmail = "abc-mail.com";
            var result = Email.Create(fakeEmail);
            Assert.True(result.IsLeft() && result.Left() == EmailError.NotValidEmail);
        }

        [Fact]
        public void Valid_email()
        {
            string email = "myemail@mail.com";
            var result = Email.Create(email);
            Assert.True(result.IsRight());
        }
    }
}

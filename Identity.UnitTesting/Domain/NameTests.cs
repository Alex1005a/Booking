using Identity.Domain.AggregatesModel.UserAggregate;
using Monad;
using Xunit;

namespace Identity.UnitTesting.Domain
{
    public class NameTests
    {
        [Fact]
        public void Too_long_Name_error()
        {
            string longName = new('a', Name.MaxNameLength + 1);
            var result = Name.Create(longName);
            Assert.True(result.IsLeft() && result.Left() == NameError.TooLongName);
        }

        [Fact]
        public void Not_valid_name_error()
        {
            string fakeName = "abc$!#";
            var result = Name.Create(fakeName);
            Assert.True(result.IsLeft() && result.Left() == NameError.NotValidName);
        }

        [Fact]
        public void Valid_name()
        {
            var result = Name.Create("Alexander");
            Assert.True(result.IsRight());
        }
    }
}

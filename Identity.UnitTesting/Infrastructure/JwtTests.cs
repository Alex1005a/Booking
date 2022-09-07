using Identity.Application;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Infrastructure;
using Monad;
using Xunit;

namespace Identity.UnitTesting.Infrastructure
{
    public class JwtTests
    {
        [Fact]
        public void Jwt_test()
        {
            var userJwt = new UserJwt(UserId.Generate(), Name.Create("Name").Right());
            var jwtAuthentication = new JwtAuthentication();

            var jwt = jwtAuthentication.GenerateJwt(userJwt);
            var user = jwtAuthentication.DecodeJwt(jwt);

            Assert.Equal(userJwt.Name.Value, user.Name.Value);
            Assert.Equal(userJwt.Id.Value, user.Id.Value);
        }
    }
}

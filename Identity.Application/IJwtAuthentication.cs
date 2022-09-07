using Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Application
{
    public interface IJwtAuthentication
    {
        string GenerateJwt(UserJwt user);
        UserJwt DecodeJwt(string jwt);
    }
    public record UserJwt(UserId Id, Name Name);
}

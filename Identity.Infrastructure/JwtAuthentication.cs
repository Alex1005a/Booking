using Identity.Application;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Monad;

namespace Identity.Infrastructure
{
    public class JwtAuthentication : IJwtAuthentication
    {
        private const string IdClaim = "Id";
        private const string NameClaim = "Name";

        public UserJwt DecodeJwt(string jwt)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            return new UserJwt(new UserId(jwtSecurityToken.Claims.First(claim => claim.Type == IdClaim).Value), 
                               Name.Create(jwtSecurityToken.Claims.First(claim => claim.Type == NameClaim).Value).Right());
        }

        public string GenerateJwt(UserJwt user)
        {
            var now = DateTime.UtcNow;
            var identity = GetIdentityClaims(user.Id, user.Name);
            var jwt = new JwtSecurityToken(
                    issuer: "ISSUER",
                    audience: "AUDIENCE",
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(30)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("hruitghrpitogtrojrg[grrgereergewffvdggrf")), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private ClaimsIdentity GetIdentityClaims(UserId userId, Name name)
        {
            var claims = new List<Claim>
                {
                    new Claim(IdClaim, userId.Value),
                    new Claim(NameClaim, name.Value)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}

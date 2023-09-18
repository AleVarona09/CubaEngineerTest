using Microsoft.IdentityModel.Tokens;
using NextPermutation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NextPermutation.Core
{
    public class AccesTokenGenerator
    {
        private readonly AuthenticationConfiguration _authentication;
        private readonly IAccesToken _accesToken;

        public AccesTokenGenerator(AuthenticationConfiguration authentication, IAccesToken accesToken)
        {
            _authentication = authentication;
            _accesToken = accesToken;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            return _accesToken.GenerateToken(
                _authentication.AccesTokenSecretKey,
                _authentication.Issuer,
                _authentication.Audience,
                _authentication.AccesTokenExpMin,
                claims);
        }
    }
}

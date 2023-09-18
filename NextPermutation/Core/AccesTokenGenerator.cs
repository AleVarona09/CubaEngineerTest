using Microsoft.IdentityModel.Tokens;
using NextPermutation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NextPermutation.Core
{
    public class AccesTokenGenerator:IAccesToken
    {
        private readonly AuthenticationConfiguration _authentication;

        public AccesTokenGenerator(AuthenticationConfiguration authentication)
        {
            _authentication = authentication;
        }

        public string GenerateToken(User user)
        {
            SecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.AccesTokenSecretKey));
            SigningCredentials credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>() {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                _authentication.Issuer,
                _authentication.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_authentication.AccesTokenExpMin),
                credentials);
        
        
            return new JwtSecurityTokenHandler().WriteToken(token);
        
        }
    }
}

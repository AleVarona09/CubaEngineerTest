using Microsoft.IdentityModel.Tokens;
using NextPermutation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NextPermutation.Core
{
    public class TokenGenerator : IAccesToken
    {
        public string GenerateToken(string secretKey, string issuer, string audience, double expMin, IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expMin),
                credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

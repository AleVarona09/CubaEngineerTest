using Microsoft.IdentityModel.Tokens;
using NextPermutation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NextPermutation.Core
{
    public class RefreshTokenValidator
    {
        private readonly AuthenticationConfiguration _configuration;

        public RefreshTokenValidator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateToken(string refreshToken)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.RefreshTokenSecretKey)),
                ValidIssuer = _configuration.Issuer,
                ValidAudience = _configuration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                handler.ValidateToken(refreshToken, parameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}

using NextPermutation.Models;
using System.Security.Claims;

namespace NextPermutation.Core
{
    public class RefreshTokenGenerator
    {
        private readonly AuthenticationConfiguration _authentication;
        private readonly IAccesToken _accesToken;

        public RefreshTokenGenerator(AuthenticationConfiguration authentication, IAccesToken accesToken)
        {
            _authentication = authentication;
            _accesToken = accesToken;
        }

        public string GenerateToken()
        {
            return _accesToken.GenerateToken(
                _authentication.RefreshTokenSecretKey,
                _authentication.Issuer,
                _authentication.Audience,
                _authentication.RefreshTokenExpMin);
        }
    }
}

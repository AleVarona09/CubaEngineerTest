using NextPermutation.Models;
using System.Security.Claims;

namespace NextPermutation.Core
{
    public interface IAccesToken
    {
        string GenerateToken(string secretKey, string issuer, string audience, double expMin, IEnumerable<Claim> claims = null);
    }
}

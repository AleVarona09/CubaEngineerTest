using NextPermutation.Models;

namespace NextPermutation.Core
{
    public interface IAccesToken
    {
        string GenerateToken(User user);
    }
}

using NextPermutation.Models;

namespace NextPermutation.Core
{
    public interface IRefreshTokenRepo
    {
        Task<Task> Create (RefreshToken refreshToken);
        Task<RefreshToken> GetByToken (string token);
        Task Delete(RefreshToken token);
        Task<Task> DeleteByUserId(Guid id);
    }
}

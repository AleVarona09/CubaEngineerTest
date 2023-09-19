using NextPermutation.Models;

namespace NextPermutation.Core
{
    public interface IUserRepo
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task<User> CreateUser(User user);
        Task<User> GetById(Guid userId);
    }
}

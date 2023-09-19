using Microsoft.EntityFrameworkCore;
using NextPermutation.Core;
using NextPermutation.Models;

namespace NextPermutation.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly AuthenticationDbContext _context;

        public UserRepo(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetById(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public Task<User> GetByUsername(string username)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

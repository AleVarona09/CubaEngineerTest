using Microsoft.EntityFrameworkCore;
using NextPermutation.Core;
using NextPermutation.Models;

namespace NextPermutation.Data
{
    public class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly AuthenticationDbContext _context;

        public RefreshTokenRepo(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task<Task> Create(RefreshToken refreshToken)
        {
            refreshToken.Id = Guid.NewGuid();
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public Task Delete(RefreshToken token)
        {
            _context.RefreshTokens.Remove(token);
            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> DeleteByUserId(Guid id)
        {
            RefreshToken request = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == id);
            _context.RefreshTokens.Remove(request);
            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            RefreshToken refresh = await _context.RefreshTokens.FirstOrDefaultAsync(r=>r.Token==token);

            return refresh;
        }



    }
}

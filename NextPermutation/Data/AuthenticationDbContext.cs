using Microsoft.EntityFrameworkCore;
using NextPermutation.Models;

namespace NextPermutation.Data
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options):base(options) 
        {
        }
        
        public DbSet<User> Users { get; set; }

    }
}

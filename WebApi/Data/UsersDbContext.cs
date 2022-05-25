using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) :base (options)
        {

        }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}

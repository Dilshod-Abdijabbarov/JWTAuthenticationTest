using JWTAuthenticationTest.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthenticationTest.Data
{
    public class AuthenticationTest : DbContext
    {
        public AuthenticationTest(DbContextOptions<AuthenticationTest> options):base(options)
        {
            
        }

        public DbSet<User> Users{ get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> RoleUser { get; set; }
    }
}

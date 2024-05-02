using JWTAuthenticationTest.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthenticationTest.Data
{
    public class AuthenticationTest : DbContext
    {
        public AuthenticationTest(DbContextOptions<AuthenticationTest> options):base(options)
        {
            
        }

        public DbSet<UserModel> UserModels { get; set; }
    }
}

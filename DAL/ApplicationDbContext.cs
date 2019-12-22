
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Authentication;
using Model.Geographical;
using Model.Users;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserDistance> UserDistances { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
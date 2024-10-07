using Microsoft.EntityFrameworkCore; // Needed for Entity Framework Core
using ProfileAPI.Models; // Needed for UserProfile model

namespace ProfileAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; init; } // Table for UserProfiles
    }
}



using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Infrastructure.Persistence;

namespace MoviesAPI.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProfilesAPI.Domain;
using ProfilesAPI.Persistence.EntityConfigurations;

namespace ProfilesAPI.Persistence
{
    public class ProfilesContext : DbContext
    {
        public ProfilesContext(DbContextOptions<ProfilesContext> options)
    : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<HumanInfo> HumansInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProfileConfigurator());
            modelBuilder.ApplyConfiguration(new HumanInfoConfigurator());
        }

    }
}

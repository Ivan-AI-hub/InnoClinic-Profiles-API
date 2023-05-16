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

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HumanInfo> HumansInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorConfigurator());
            modelBuilder.ApplyConfiguration(new PatientConfigurator());
            modelBuilder.ApplyConfiguration(new HumanInfoConfigurator());
        }

    }
}

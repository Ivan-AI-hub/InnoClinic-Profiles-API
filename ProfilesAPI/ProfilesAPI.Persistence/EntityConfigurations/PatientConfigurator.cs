using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Persistence.EntityConfigurations
{
    internal class PatientConfigurator : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasOne(x => x.Info);
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Persistence.EntityConfigurations
{
    internal class DoctorConfigurator : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.OwnsOne(x => x.Office);
            builder.HasOne(x => x.Info);
        }
    }
}

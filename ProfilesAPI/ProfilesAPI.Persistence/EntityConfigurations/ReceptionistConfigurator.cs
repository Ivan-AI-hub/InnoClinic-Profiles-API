using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Persistence.EntityConfigurations
{
    public class ReceptionistConfigurator : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            builder.OwnsOne(x => x.Office);
            builder.HasOne(x => x.Info);
        }
    }
}

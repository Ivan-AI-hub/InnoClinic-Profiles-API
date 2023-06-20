using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Persistence.EntityConfigurations
{
    internal class ProfileConfigurator : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.OwnsOne(x => x.Office);
            builder.HasOne(x => x.Info);
        }
    }
}

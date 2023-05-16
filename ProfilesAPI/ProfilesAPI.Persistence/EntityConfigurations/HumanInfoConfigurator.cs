using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesAPI.Domain;

namespace ProfilesAPI.Persistence.EntityConfigurations
{
    internal class HumanInfoConfigurator : IEntityTypeConfiguration<HumanInfo>
    {
        public void Configure(EntityTypeBuilder<HumanInfo> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}

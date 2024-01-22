using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HebrewVerb.Infrastructure.Configuring;

public class VerbModelConfiguration : IEntityTypeConfiguration<VerbModel>
{
    public void Configure(EntityTypeBuilder<VerbModel> builder)
    {
        builder.HasIndex(sh => sh.Name).IsUnique();
    }
}

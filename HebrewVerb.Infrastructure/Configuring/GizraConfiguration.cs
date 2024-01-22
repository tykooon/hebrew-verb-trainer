using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HebrewVerb.Infrastructure.Configuring;

public class GizraConfiguration : IEntityTypeConfiguration<Gizra>
{
    public void Configure(EntityTypeBuilder<Gizra> builder)
    {
        builder.HasIndex(sh => sh.Name).IsUnique();
    }
}

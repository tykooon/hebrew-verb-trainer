using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HebrewVerb.Infrastructure.Configuring;

public class ShoreshConfiguration : IEntityTypeConfiguration<Shoresh>
{
    public void Configure(EntityTypeBuilder<Shoresh> builder)
    {
        builder.HasMany(sh => sh.Gizras)
            .WithMany(g => g.Shoreshes);

        builder.HasIndex(sh => sh.Short).IsUnique();


    }
}

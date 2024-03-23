using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HebrewVerb.Infrastructure.Configuring;

public class ShoreshConfiguration : IEntityTypeConfiguration<Shoresh>
{
    public void Configure(EntityTypeBuilder<Shoresh> builder)
    {
        builder.HasIndex(sh => sh.Short).IsUnique();


    }
}

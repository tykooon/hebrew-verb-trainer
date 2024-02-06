using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HebrewVerb.Infrastructure.Configuring;

public class VerbPrepositionConfiguration : IEntityTypeConfiguration<VerbPreposition>
{
    public void Configure(EntityTypeBuilder<VerbPreposition> builder)
    {
        builder.HasIndex(vp => new { vp.VerbId, vp.PrepositionId }).IsUnique();
    }
}

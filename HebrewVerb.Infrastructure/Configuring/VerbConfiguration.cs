using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HebrewVerb.Infrastructure.Configuring;

public class VerbConfiguration : IEntityTypeConfiguration<Verb>
{
    public void Configure(EntityTypeBuilder<Verb> builder)
    {
        builder.Property(v => v.Binyan)
            .HasConversion(
                b => b.Value,
                v => Binyan.FromValue(v));

        builder.HasMany(v => v.Prepositions)
            .WithMany(p => p.Verbs)
            .UsingEntity<VerbPreposition>();
    }
}

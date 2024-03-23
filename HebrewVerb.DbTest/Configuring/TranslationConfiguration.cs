using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HebrewVerb.Infrastructure.Configuring;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.Property(t => t.Language)
            .HasConversion<int>();

        builder.Property(v => v.Tags)
            .HasConversion(
            new ValueConverter<ICollection<VerbTag>, int>(
                list => list.GetVerbTagFlags(),
                flags => flags.GetVerbTags().ToList()),
            new ValueComparer<ICollection<VerbTag>>(
                (b1, b2) => (b1 != null) && (b2 != null) && b1.GetVerbTagFlags().Equals(b2.GetVerbTagFlags()),
                b1 => b1.GetVerbTagFlags())
            );
    }
}

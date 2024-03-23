using HebrewVerb.DbTest.Configuring;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HebrewVerb.Infrastructure.Configuring;

public class VerbConfiguration : IEntityTypeConfiguration<Verb>
{
    public void Configure(EntityTypeBuilder<Verb> builder)
    {
        builder.Property(v => v.Binyan)
            .HasConversion(
                b => b.Value,
                v => Binyan.FromValue(v));

        builder.Property(v => v.Tags)
            .HasConversion(
            new ValueConverter<ICollection<VerbTag>, int>(
                list => list.GetVerbTagFlags(),
                flags => flags.GetVerbTags().ToList()),
            new ValueComparer<ICollection<VerbTag>>(
                (b1, b2) => (b1 != null) && (b2 != null) && b1.GetVerbTagFlags().Equals(b2.GetVerbTagFlags()),
                b1 => b1.GetVerbTagFlags())
            );

        builder.Property(v => v.ExtraInfo)
            .HasConversion(
        new ValueConverter<Dictionary<string, string>, string>(
            info => info.ToJson(),
            json => json.ToExtraInfo()),
        new ValueComparer<Dictionary<string, string>>(
            (f1, f2) => ExtraInfoService.EqualsDictionary(f1, f2),
            f => ExtraInfoService.ExtraInfoHashCode(f))
        );
    }
}

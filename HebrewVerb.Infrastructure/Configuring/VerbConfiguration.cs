using HebrewVerb.Application.Services;
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
                b => b.Id,
                v => Binyan.FromId(v));

        builder.Property(v => v.Tags)
            .HasConversion(
                new ValueConverter<ICollection<VerbTag>, int>(
                    list => list.GetFlagSum(),
                    flags => VerbTag.GetTagsFromFlag(flags).ToList()),
                new ValueComparer<ICollection<VerbTag>>(
                    (b1, b2) => (b1 != null) && (b2 != null) && b1.GetFlagSum().Equals(b2.GetFlagSum()),
                    b1 => b1.GetFlagSum())
            );

        builder.Property(v => v.Gizras)
            .HasConversion(
                new ValueConverter<ICollection<Gizra>, int>(
                    list => list.GetFlagSum(),
                    flags => Gizra.GetTagsFromFlag(flags).ToList()),
                new ValueComparer<ICollection<Gizra>>(
                    (b1, b2) => (b1 != null) && (b2 != null) && b1.GetFlagSum().Equals(b2.GetFlagSum()),
                    b1 => b1.GetFlagSum())
            );

        builder.Property(v => v.VerbModels)
            .HasConversion(
                new ValueConverter<ICollection<VerbModel>, int>(
                    list => list.GetFlagSum(),
                    flags => VerbModel.GetTagsFromFlag(flags).ToList()),
                new ValueComparer<ICollection<VerbModel>>(
                    (b1, b2) => (b1 != null) && (b2 != null) && b1.GetFlagSum().Equals(b2.GetFlagSum()),
                    b1 => b1.GetFlagSum())
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

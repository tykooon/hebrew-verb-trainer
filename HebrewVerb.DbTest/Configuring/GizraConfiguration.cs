using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;

namespace HebrewVerb.Infrastructure.Configuring;

public class GizraConfiguration : IEntityTypeConfiguration<Gizra>
{
    public void Configure(EntityTypeBuilder<Gizra> builder)
    {
        builder.HasIndex(sh => sh.Name).IsUnique();

        builder.Property(g => g.Binyans).HasConversion(
            new ValueConverter<ICollection<Binyan>, byte>(
                list => list.GetBinyanFlags(),
                flags => flags.GetBinyans().ToList()),
            new ValueComparer<ICollection<Binyan>>(
                (b1, b2) => (b1 != null) && (b2 !=null) && b1.GetBinyanFlags().Equals(b2.GetBinyanFlags()),
                b1 => b1.GetBinyanFlags())
    );
    }
}

using HebrewVerb.Application.Models;
using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HebrewVerb.Infrastructure.Configuring;

public class FilterConfiguration : IEntityTypeConfiguration<AppFilter>
{
    public void Configure(EntityTypeBuilder<AppFilter> builder)
    {
        builder.Property(f => f.Filter).HasConversion(
        new ValueConverter<Filter, string>(
            filter => filter.ToJson(),
            json => Filter.FromJson(json)),
        new ValueComparer<Filter>(
            (f1, f2) => FilterExtensions.EqualsTo(f1, f2),
            f => FilterExtensions.GetHashCode(f)));
    }
}

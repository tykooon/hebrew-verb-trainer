using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Feature.Shoreshes;

namespace HebrewVerb.Application.Common.Mappers;

public static class ShoreshMapper
{
    public static Shoresh ToShoresh(this ShoreshDto dto) => new(dto.ShortName);

    public static ShoreshDto ToDto(this Shoresh shoresh) => new()
    {
        Id = shoresh.Id,
        ShortName = shoresh.Short,
        NameWithDots = shoresh.WithDots,
        VerbIds = shoresh.Verbs.Select(v => v.Id)
    };
}

using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Extensions;

namespace HebrewVerb.Application.Common.Mappers;

public static class GizraMapper
{
    public static Gizra ToGizra(this GizraDto dto) => new(dto.Name, dto.Description, [.. dto.Binyans.GetBinyans()]);

    public static GizraDto ToDto(this Gizra gizra) => new()
    {
        Id = gizra.Id,
        Name = gizra.Name,
        Description = gizra.Description ?? string.Empty,
        Binyans = gizra.Binyans.GetBinyanNames(),
    };

    public static IEnumerable<string> GetGizraNames(this IEnumerable<GizraDto> gizras) =>
        gizras.Select(g => g.Name);
}

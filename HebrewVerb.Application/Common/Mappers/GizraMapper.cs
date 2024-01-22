using HebrewVerb.Domain.Enums;
using Ardalis.SmartEnum;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Feature.Gizras;

namespace HebrewVerb.Application.Common.Mappers;

public static class GizraMapper
{
    public static Gizra ToGizra(this GizraDto dto) => new(dto.Name, dto.Description);

    public static GizraDto ToDto(this Gizra gizra) => new()
    {
        Name = gizra.Name,
        Description = gizra.Description ?? string.Empty,
    };
}

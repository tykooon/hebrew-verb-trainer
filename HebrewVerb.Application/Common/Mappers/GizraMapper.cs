using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Common.Mappers;

public static class GizraMapper
{
    public static Gizra ToGizra(this GizraDto dto) => new(dto.Name, dto.Description);

    public static GizraDto ToDto(this Gizra gizra) => new()
    {
        Id = gizra.Id,
        Name = gizra.Name,
        Description = gizra.Description ?? string.Empty,
    };
}

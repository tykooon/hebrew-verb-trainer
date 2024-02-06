using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Common.Mappers;

public static class VerbModelMapper
{
    public static VerbModel ToGizra(this VerbModelDto dto) => new(dto.Name, dto.Description);

    public static VerbModelDto ToDto(this VerbModel verbModel) => new()
    {
        Id = verbModel.Id,
        Name = verbModel.Name,
        Description = verbModel.Description ?? string.Empty,
    };
}

using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Extensions;

namespace HebrewVerb.Application.Common.Mappers;

public static class VerbModelMapper
{
    public static VerbModel ToVerbModel(this VerbModelDto dto) => new(dto.Name, dto.Description, [.. dto.Binyans.GetBinyans()]);

    public static VerbModelDto ToDto(this VerbModel verbModel) => new()
    {
        Id = verbModel.Id,
        Name = verbModel.Name,
        Description = verbModel.Description ?? string.Empty,
        Binyans = verbModel.Binyans.GetBinyanNames(),
    };

    public static IEnumerable<string> GetVerbModelsNames(this IEnumerable<VerbModelDto> verbModels) =>
    verbModels.Select(g => g.Name);
}

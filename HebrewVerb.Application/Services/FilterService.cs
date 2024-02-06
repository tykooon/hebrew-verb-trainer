using HebrewVerb.Application.Common.Enums;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Gizras.Queries;
using HebrewVerb.Application.Feature.VerbModels.Queries;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Enums;
using MediatR;

namespace HebrewVerb.Application.Services;

public class FilterService
{
    public static IEnumerable<BinyanDto> GetBinyans () =>
        Binyan.List.Where(b => b != Binyan.Undefined).ToDtoList();

    public static BinyanDto? GetBinyanInfo(string name, AppLanguage lang = AppLanguage.Russian) =>
        Binyan.TryFromName(name, out var info)
            ? info.ToDto(lang)
            : null;

    public static IEnumerable<ZmanDto> GetZmans() =>
        Zman.List.Where(b => b != Zman.Infinitive).ToDtoList();

    public static ZmanDto? GetZmanInfo(string name, AppLanguage lang = AppLanguage.Russian) =>
        Zman.TryFromName(name, out var info)
            ? info.ToDto(lang)
            : null;

    public static async Task<IEnumerable<GizraDto>> GetGizras(IMediator mediator)
    {
        var res = await mediator.Send(new GetAllGizrasQuery());
        return res ?? [];
    }

    public static async Task<IEnumerable<VerbModelDto>> GetVerbModels(IMediator mediator)
    {
        var res = await mediator.Send(new GetAllVerbModelsQuery());
        return res ?? [];
    }



}

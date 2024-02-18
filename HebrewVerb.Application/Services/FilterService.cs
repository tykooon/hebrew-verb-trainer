using HebrewVerb.Application.Feature.Gizras.Queries;
using HebrewVerb.Application.Feature.VerbModels.Queries;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Services;

public class FilterService
{
    public static readonly HashSet<Binyan> BinyanOptions = [
        Binyan.Paal,
        Binyan.Piel,
        Binyan.Hifil,
        Binyan.Hitpael,
        Binyan.Nifal,
        Binyan.Pual,
        Binyan.Hufal];

    public static readonly HashSet<Zman> ZmanOptions = [
        Zman.Past,
        Zman.Present,
        Zman.Future,
        Zman.Imperative];

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

using HebrewVerb.Application.Models;
using HebrewVerb.BlazorApp.Services;

namespace HebrewVerb.BlazorApp.Components.Tools;

public partial class FilterBar
{
    private IEnumerable<BinyanDto> BinyanList { get; set; } = [];
    private IEnumerable<GizraDto> GizraList { get; set; } = [];
    private IEnumerable<VerbModelDto> VerbModelList { get; set; } = [];
    private IEnumerable<ZmanDto> ZmanList { get; set; } = [];

    public async Task RefreshFilter()
    {
        GizraList = await _cache.GetGizraList(_mediator) ?? [];
        Gizras = GizraList.Where(g => CurrentFilter.Gizras.Contains(g));

        VerbModelList = await _cache.GetVerbModelList(_mediator) ?? [];
        VerbModels = VerbModelList.Where(vm => CurrentFilter.VerbModels.Contains(vm));
        StateHasChanged();
    }

    private async Task ExpandedChanged(bool newVal)
    {
        if (newVal)
        {
            await Task.Delay(10);
        }
        else
        {
            // Reset after a while to prevent sudden collapse.
            await Task.Delay(300);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        BinyanList = _cache.GetBinyanList() ?? [];
        Binyans = BinyanList.Where(b => CurrentFilter.Binyans.Select(x => x.Name).Contains(b.Name));

        ZmanList = _cache.GetZmanList() ?? []; ;
        Zmans = ZmanList.Where(z => CurrentFilter.Zmans.Contains(z));

        GizraList = await _cache.GetGizraList(_mediator) ?? [];
        Gizras = GizraList.Where(g => CurrentFilter.Gizras.Contains(g));

        VerbModelList = await _cache.GetVerbModelList(_mediator) ?? [];
        VerbModels = VerbModelList.Where(vm => CurrentFilter.VerbModels.Contains(vm));
    }
}


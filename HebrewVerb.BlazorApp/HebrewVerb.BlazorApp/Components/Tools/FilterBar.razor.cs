using HebrewVerb.Application.Models;
using HebrewVerb.Application.Common.Enums;
using HebrewVerb.BlazorApp.Services;
using HebrewVerb.SharedKernel.Extensions;


namespace HebrewVerb.BlazorApp.Components.Tools;

public partial class FilterBar
{
    public async Task RefreshFilter(bool firstRender = false)
    {
        AllowedGizras = await _cache.GetGizraList(_mediator) ?? [];
        AllowedVerbModels = await _cache.GetVerbModelList(_mediator) ?? [];

        var currentBinyans = CurrentFilter.Binyans.GetBinyanNames();
        if (currentBinyans.Any())
        {
            AllowedGizras = AllowedGizras.Where(g => currentBinyans.Intersect(g.Binyans).Any());
            AllowedVerbModels = AllowedVerbModels.Where(vm => currentBinyans.Intersect(vm.Binyans).Any());
        }
        CurrentFilter.Gizras = CurrentFilter.Gizras.Intersect(AllowedGizras).ToHashSet();
        CurrentFilter.VerbModels = CurrentFilter.VerbModels.Intersect(AllowedVerbModels).ToHashSet();

        if (!firstRender)
        {
            StateHasChanged();
        }
    }

    public async Task ExpandedChanged(bool newVal)
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
        await RefreshFilter(true);

        // TODO GetCurrentUserDetails from injected service
        _currentUserDetails = new(0, "testuser", "test@hebverb.info", [""])
        {
            Status = AppUserStatus.Basic
        };
        // TODO Get NameList of UserFilters if possible

    }
}


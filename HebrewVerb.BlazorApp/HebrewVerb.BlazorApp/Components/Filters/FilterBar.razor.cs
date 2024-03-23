using HebrewVerb.Application.Common.Enums;
using HebrewVerb.BlazorApp.Common;
using HebrewVerb.SharedKernel.Extensions;


namespace HebrewVerb.BlazorApp.Components.Filters;

public partial class FilterBar
{
    public void RefreshFilter(bool firstRender = false)
    {
        AllowedVerbModels = FilterOptions.VerbModelOptions;
        AllowedGizras = FilterOptions.GizraOptions;

        var currentBinyans = CurrentFilter.Binyans.GetBinyanNames();
        if (currentBinyans.Any())
        {
            AllowedGizras = AllowedGizras.Where(g => CurrentFilter.Binyans.Intersect(g.Binyans).Any());
            AllowedVerbModels = AllowedVerbModels.Where(vm => CurrentFilter.Binyans.Intersect(vm.Binyans).Any());
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
            await Task.Delay(10);
        else
            // Reset after a while to prevent sudden collapse.
            await Task.Delay(300);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        RefreshFilter(true);

        // TODO GetCurrentUserDetails from injected service
        _currentUserDetails = new(0, "testuser", "test@hebverb.info", [""])
        {
            Status = AppUserStatus.Basic
        };
        // TODO Get NameList of UserFilters if possible

    }
}


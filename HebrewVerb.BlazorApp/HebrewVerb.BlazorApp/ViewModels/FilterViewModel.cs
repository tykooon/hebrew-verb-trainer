using HebrewVerb.Application.Models;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;

namespace HebrewVerb.BlazorApp.ViewModels;

public class FilterViewModel
{
    public string FilterName { get; set; } = "Default";
    public bool IsChanged { get; set; } = false;
    public HashSet<Binyan> Binyans { get; set; } = [];
    public HashSet<Gizra> Gizras { get; set; } = [];
    public HashSet<VerbModel> VerbModels { get; set; } = [];
    public HashSet<VerbTag> VerbTags { get; set; } = [];
    public HashSet<Zman> Zmans { get; set; } = [];
    public int VerbLimit { get; set; } = 10;

    public Filter ToFilter() => Filter.FromParams(
            Binyans.GetBinyanNames(),
            Gizras.Select(g => g.Id),
            VerbModels.Select(vm => vm.Id),
            VerbTags.Select(tag => tag.Id),
            Zmans.GetTagNames(Language.English),
            VerbLimit);
}

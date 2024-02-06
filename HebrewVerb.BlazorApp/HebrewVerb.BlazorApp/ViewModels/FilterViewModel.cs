using HebrewVerb.Application.Models;

namespace HebrewVerb.BlazorApp.ViewModels;

public class FilterViewModel
{
    public IEnumerable<BinyanDto> Binyans { get; set; } = [];
    public IEnumerable<GizraDto> Gizras { get; set; } = [];
    public IEnumerable<VerbModelDto> VerbModels { get; set; } = [];
    public IEnumerable<ZmanDto> Zmans { get; set; } = [];
}

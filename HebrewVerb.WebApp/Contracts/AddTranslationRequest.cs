using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.WebApp.Contracts;

public class AddTranslationRequest
{
    public string? Lang { get; set; } = "rus";
    public string Main { get; set; } = "";
    public string Aux { get; set; } = "";
    public List<string> Tag { get; set; } = [];
    public IEnumerable<int> PrepositionIds { get; set; } = [];
}

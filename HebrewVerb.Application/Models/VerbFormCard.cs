namespace HebrewVerb.Application.Models;

public class VerbFormCard
{
    public string Zman { get; set; } = string.Empty;
    public string Guf { get; set; } = string.Empty;
    public string Infinitive { get; set; } = string.Empty;
    public string VerbFormHebrew { get; set; } = string.Empty;
    public string VerbFormTranslit { get; set; } = string.Empty;
    public int TranslitStress { get; set; }
    public string Translation { get; set; } = string.Empty;
    public IEnumerable<string> Gizras { get; set; } = [];
    public IEnumerable<string> VerbModels { get; set; } = [];
}

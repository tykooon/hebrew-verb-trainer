namespace HebrewVerb.Application.Models;

public class VerbFormCard
{
    public int VerbId {  get; set; }
    public string Zman { get; set; } = string.Empty;
    public string Guf { get; set; } = string.Empty;
    public string VerbFormHebrew { get; set; } = string.Empty;
    public string VerbFormHebrewNikkud { get; set; } = string.Empty;
    public string VerbFormTranslit { get; set; } = string.Empty;
    public int TranslitStress { get; set; }
}

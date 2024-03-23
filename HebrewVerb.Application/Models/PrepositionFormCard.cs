namespace HebrewVerb.Application.Models;

public class PrepositionFormCard
{
    public int Id {  get; set; }
    public string Guf { get; set; } = string.Empty;
    public string PrepFormHebrew { get; set; } = string.Empty;
    public string PrepFormHebrewNikkud { get; set; } = string.Empty;
    public string PrepFormTranslit { get; set; } = string.Empty;
    public int TranslitStress { get; set; }
}

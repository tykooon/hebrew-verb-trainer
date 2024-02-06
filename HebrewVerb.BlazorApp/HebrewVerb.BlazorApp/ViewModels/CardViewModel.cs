using HebrewVerb.Application.Common.Enums;
using HebrewVerb.BlazorApp.Common;
using MudBlazor;
namespace HebrewVerb.BlazorApp.ViewModels;

public class CardViewModel
{
    public bool IsAnswered { get; set; } = false;
    public string Zman { get; set; } = "הווה";
    public string Infinitive { get; set; } = "לעשות";
    public string Guf { get; set; } = "היא";
    public string VerbForm { get; set; } = "עושה";
    public string VerbFormTranslit { get; set; } = "осэ";
    public int VerbFormStress { get; set; } = 2;
    public string Binyan { get; set; } = "פעל";
    public IEnumerable<string> Gizras { get; set; } = ["ל''ה/י", "פ''ע"];
    public IEnumerable<string> Models { get; set; } = ["исключение", "лит."];
    public string Translation { get; set; } = "делать";
    public AppLanguage Lang { get; set; } = AppLanguage.Russian;

    public Color CardColor => Constants.ZmanColor[Zman];
}

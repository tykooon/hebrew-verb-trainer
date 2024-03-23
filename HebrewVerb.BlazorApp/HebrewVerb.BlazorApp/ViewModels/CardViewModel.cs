using HebrewVerb.Application.Models;
using HebrewVerb.BlazorApp.Common;
using HebrewVerb.BlazorApp.Common.Extensions;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MudBlazor;

namespace HebrewVerb.BlazorApp.ViewModels;

public class CardViewModel
{
    public bool IsAnswered { get; set; } = false;
    public string Zman { get; set; } = "הווה";
    public string Infinitive { get; set; } = "לעשות";
    public string Guf { get; set; } = "היא";
    public string VerbForm { get; set; } = "עושה";
    public string VerbFormNikkud { get; set; } = "עושה";
    public string VerbFormTranslit { get; set; } = "осэ";
    public int VerbFormStress { get; set; } = 2;
    public string Binyan { get; set; } = "פעל";
    public IEnumerable<string> Gizras { get; set; } = ["ל''ה/י", "פ''ע"];
    public IEnumerable<string> Models { get; set; } = ["исключение", "лит."];
    public IEnumerable<VerbTag> Tags { get; set; } = [];
    public string Translation { get; set; } = "делать";
    public Language Lang { get; set; } = Language.Russian;

    public Color CardColor => Constants.ZmanColor[Zman];

    public CardViewModel() { }

    public CardViewModel(VerbFormCard card, VerbInfo verb)
    {
        Zman = card.Zman;
        Infinitive = verb.Infinitive;
        Guf = card.Guf;
        VerbForm = card.VerbFormHebrew;
        VerbFormNikkud = card.VerbFormHebrewNikkud;
        VerbFormTranslit = card.VerbFormTranslit;
        VerbFormStress = card.TranslitStress;
        Binyan = verb.Binyan.ToString(Language.Russian);
        Gizras = verb.Gizras.GetTagNames(Language.Hebrew);
        Models = verb.VerbModels.GetTagNames(Language.Hebrew);
        Tags = verb.Tags;
        Translation = verb.AllTranslations();
    }

}

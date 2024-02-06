using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Enums;

namespace HebrewVerb.Domain.Entities;

public class Translation : BaseEntity<int>
{
    public readonly static Translation Default = new();

    public string Russian { get; private set; } = "";
    public string English { get; private set; } = "";
    public string RussianShort { get; private set; } = "";
    public string EnglishShort { get; private set; } = "";

    public string Get(Lang lang = Lang.Russian, bool isShort = false) =>
        lang switch
        {
            Lang.Russian => isShort ? RussianShort : Russian,
            Lang.English => isShort ? EnglishShort : English,
            _ => isShort ? RussianShort : Russian,
        };

    public Translation(string russian, string english, string russianShort, string englishShort)
    {
        Russian = russian;
        English = english;
        EnglishShort = englishShort;
        RussianShort = russianShort;
    }

    public Translation() { }

    public void Set(string value, Lang lang = Lang.Russian, bool isShort = false)
    {
        switch (lang)
        {
            case Lang.Russian:
                if(isShort)
                {
                    RussianShort = value;
                }
                else
                {
                    Russian = value;
                }
                break;
            case Lang.English:
                if (isShort)
                {
                    EnglishShort = value;
                }
                else
                {
                    English = value;
                }
                break;
        }
    }
}

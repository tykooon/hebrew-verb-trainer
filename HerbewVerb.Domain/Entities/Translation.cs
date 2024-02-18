using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Domain.Entities;

public class Translation : BaseEntity<int>
{
    public readonly static Translation Default = new();

    public string Russian { get; private set; } = "";
    public string English { get; private set; } = "";
    public string RussianShort { get; private set; } = "";
    public string EnglishShort { get; private set; } = "";

    public string Get(Language lang = Language.Russian, bool isShort = false) =>
        lang switch
        {
            Language.Russian => isShort ? RussianShort : Russian,
            Language.English => isShort ? EnglishShort : English,
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

    public void Set(string value, Language lang = Language.Russian, bool isShort = false)
    {
        switch (lang)
        {
            case Language.Russian:
                if(isShort)
                {
                    RussianShort = value;
                }
                else
                {
                    Russian = value;
                }
                break;
            case Language.English:
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

using HebrewVerb.SharedKernel.Enums;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace HebrewVerb.WebApp.Contracts;

public static class ContractHelpers
{
    public static Language? ToLanguage(this string lang)
    {
        if (string.IsNullOrEmpty(lang))
        {
            return null;
        }

        return lang.ToLower() switch
        {
            "rus" => Language.Russian,
            "eng" => Language.English,
            "heb" => Language.Hebrew,
            "esp" => Language.Spanish,
            "all" => Language.All,
            _ => null
        };
    }
}

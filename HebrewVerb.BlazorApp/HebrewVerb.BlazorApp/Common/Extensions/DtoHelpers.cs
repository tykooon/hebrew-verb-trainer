using HebrewVerb.Application.Models;

namespace HebrewVerb.BlazorApp.Common.Extensions;

public static class DtoHelpers
{
    public static string AllTranslations(this VerbDto dto)
    {
        if (dto == null)
        {
            return string.Empty;
        }

        return dto.Translation;
    }

    public static string AllTranslations(this VerbInfo dto)
    {
        if (dto == null)
        {
            return string.Empty;
        }

        return string.Join(", ", dto.Translations);
    }
}

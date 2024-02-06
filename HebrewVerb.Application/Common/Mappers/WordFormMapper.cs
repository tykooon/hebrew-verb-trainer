using HebrewVerb.Application.Common.Enums;
using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.Domain.Entities;

namespace HebrewVerb.Application.Common.Mappers;

public static class WordFormMapper
{
    public static WordForm FromDto(this WordFormDto dto, AppLanguage lang = AppLanguage.Russian)
    {
        WordForm result = lang switch
        {
            AppLanguage.Russian => new(dto.Hebrew, dto.Hebrew, dto.Transcript, dto.Stress, "", 0),
            AppLanguage.English => new(dto.Hebrew, dto.Hebrew, "", 0, dto.Transcript, dto.Stress),
            _ => new(dto.Hebrew, dto.Hebrew),
        };

        return result;
    }

    public static WordFormDto ToDto(this WordForm wf, AppLanguage lang = AppLanguage.Russian)
    {
        WordFormDto result = lang switch
        {
            AppLanguage.Russian => new(wf.HebrewNiqqud ?? "", wf.TranscriptionRus ?? "", wf.StressLetterRus),
            AppLanguage.English => new(wf.HebrewNiqqud ?? "", wf.TranscriptionEng ?? "", wf.StressLetterEng),
            _ => new(wf.HebrewNiqqud ?? "", string.Empty, -1),
        };

        return result;
    }
}

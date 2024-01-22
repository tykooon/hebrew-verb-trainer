using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class WordFormMapper
{
    public static WordForm FromDto(this WordFormDto dto, Lang lang = Lang.Russian)
    {
        WordForm result = lang switch
        {
            Lang.Russian => new(dto.Hebrew, dto.Hebrew, dto.Transcript, dto.Stress, "", 0),
            Lang.English => new(dto.Hebrew, dto.Hebrew, "", 0, dto.Transcript, dto.Stress),
            _ => new(dto.Hebrew, dto.Hebrew),
        };

        return result;
    }

    public static WordFormDto ToDto(this WordForm wf, Lang lang = Lang.Russian)
    {
        WordFormDto result = lang switch
        {
            Lang.Russian => new(wf.HebrewNiqqud ?? "", wf.TranscriptionRus ?? "", wf.StressLetterRus),
            Lang.English => new(wf.HebrewNiqqud ?? "", wf.TranscriptionEng ?? "", wf.StressLetterEng),
            _ => new(wf.HebrewNiqqud ?? "", string.Empty, -1),
        };

        return result;
    }
}

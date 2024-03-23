using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class WordFormMapper
{
    public static WordForm FromDto(this WordFormDto dto, Language lang = Language.Russian)
    {
        WordForm result = lang switch
        {
            Language.Russian => new(dto.Hebrew, dto.HebrewNikkud, dto.Transcript, dto.Stress, "", 0),
            Language.English => new(dto.Hebrew, dto.HebrewNikkud, "", 0, dto.Transcript, dto.Stress),
            _ => new(dto.Hebrew, dto.Hebrew),
        };

        return result;
    }

    public static WordFormDto ToDto(this WordForm wf, Language lang = Language.Russian)
    {
        WordFormDto result = lang switch
        {
            Language.Russian => new(wf.Hebrew, wf.HebrewNikkud, wf.TranscriptionRus ?? "", wf.StressLetterRus),
            Language.English => new(wf.Hebrew, wf.HebrewNikkud, wf.TranscriptionEng ?? "", wf.StressLetterEng),
            _ => new(wf.Hebrew, wf.HebrewNikkud, string.Empty, -1),
        };

        return result;
    }
}

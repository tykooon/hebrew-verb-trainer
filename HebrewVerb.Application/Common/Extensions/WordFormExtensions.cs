using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Common.Extensions;

public static class WordFormExtensions
{
    public static void UpdateFromDto(this WordForm wordForm, WordFormDto? dto, Language lang)
    {
        if (dto == null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(dto.Hebrew))
        {
            wordForm.Hebrew = dto.Hebrew;
        }

        if (!string.IsNullOrEmpty(dto.HebrewNikkud))
        {
            wordForm.HebrewNiqqud = dto.HebrewNikkud;
        }

        if (!string.IsNullOrEmpty(dto.Transcript))
        {
            switch (lang) {
                case Language.English:
                    wordForm.AddTranscriptionEng(dto.Transcript, dto.Stress);
                    break;
                case Language.Russian:
                    wordForm.AddTranscriptionRus(dto.Transcript, dto.Stress);
                    break;
            }
        }
    }
}

using HebrewVerb.Domain.Entities;
using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Common.Mappers;

public static class TranslationMapper
{
    public static Translation ToTranslation(this TranslationDto dto, params Preposition[] preps)
    {
        var translation = new Translation(dto.Language, dto.Main);
        translation.Update(null, dto.Auxillare, preps);
        translation.UpdateTags([.. dto.Tags]);
        return translation;
    }

    public static TranslationDto ToDto(this Translation translation) =>
        new(
            translation.Id,
            translation.Language,
            translation.VerbId,
            translation.Main,
            translation.Auxillare,
            translation.Tags,
            translation.Prepositions.Select(p => p.Id));
}

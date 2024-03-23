using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class VerbMapper
{
    public static VerbDto ToDto(this Verb verb, Language lang)
    {
        var result = new VerbDto()
        {
            Id = verb.Id,
            Infinitive = verb.Infinitive.ToDto(lang),
            Binyan = verb.Binyan.ToString(lang),
            Shoresh = verb.Shoresh.WithDots,
            Translation = verb.TranslationFull(lang),
            LangId = (int)lang,
            PresentMs = verb.Present?.MS.ToDto(lang),
            PresentFs = verb.Present?.FS.ToDto(lang),
            PresentMp = verb.Present?.MP.ToDto(lang),
            PresentFp = verb.Present?.FP.ToDto(lang),

            PastMs1 = verb.Past?.MS1.ToDto(lang),
            PastMp1 = verb.Past?.MP1.ToDto(lang),
            PastMs2 = verb.Past?.MS2.ToDto(lang),
            PastFs2 = verb.Past?.FS2.ToDto(lang),
            PastMp2 = verb.Past?.MP2.ToDto(lang),
            PastFp2 = verb.Past?.FP2.ToDto(lang),
            PastMs3 = verb.Past?.MS3.ToDto(lang),
            PastFs3 = verb.Past?.FS3.ToDto(lang),
            PastMp3 = verb.Past?.MP3.ToDto(lang),

            FutureMs1 = verb.Future?.MS1.ToDto(lang),
            FutureMp1 = verb.Future?.MP1.ToDto(lang),
            FutureMs2 = verb.Future?.MS2.ToDto(lang),
            FutureFs2 = verb.Future?.FS2.ToDto(lang),
            FutureMp2 = verb.Future?.MP2.ToDto(lang),
            FutureMs3 = verb.Future?.MS3.ToDto(lang),
            FutureMp3 = verb.Future?.MP3.ToDto(lang),

            ImperativeMs = verb.Imperative?.MS?.ToDto(lang),
            ImperativeFs = verb.Imperative?.FS?.ToDto(lang),
            ImperativeMp = verb.Imperative?.MP?.ToDto(lang),
            ExtraInfo = verb.ExtraInfo,
            Gizras = [.. verb.Gizras],
            VerbModels = [.. verb.VerbModels],
            Tags = [.. verb.Tags],
        };
        return result;
    }

    public static VerbDto ToDtoWithUpdate(this Verb verb, Language lang,
        Shoresh? shoresh = null,
        Past? past = null,
        Present? present = null,
        Future? future = null,
        Imperative? imperative = null)
    {
        var result = new VerbDto()
        {
            Id = verb.Id,
            Infinitive = verb.Infinitive.ToDto(lang),
            Binyan = verb.Binyan.ToString(lang),
            Shoresh = shoresh?.WithDots ?? string.Empty,
            Translation = verb.TranslationFull(lang),
            LangId = (int)lang,
            PresentMs = present?.MS.ToDto(lang),
            PresentFs = present?.FS.ToDto(lang),
            PresentMp = present?.MP.ToDto(lang),
            PresentFp = present?.FP.ToDto(lang),

            PastMs1 = past?.MS1.ToDto(lang),
            PastMp1 = past?.MP1.ToDto(lang),
            PastMs2 = past?.MS2.ToDto(lang),
            PastFs2 = past?.FS2.ToDto(lang),
            PastMp2 = past?.MP2.ToDto(lang),
            PastFp2 = past?.FP2.ToDto(lang),
            PastMs3 = past?.MS3.ToDto(lang),
            PastFs3 = past?.FS3.ToDto(lang),
            PastMp3 = past?.MP3.ToDto(lang),

            FutureMs1 = future?.MS1.ToDto(lang),
            FutureMp1 = future?.MP1.ToDto(lang),
            FutureMs2 = future?.MS2.ToDto(lang),
            FutureFs2 = future?.FS2.ToDto(lang),
            FutureMp2 = future?.MP2.ToDto(lang),
            FutureMs3 = future?.MS3.ToDto(lang),
            FutureMp3 = future?.MP3.ToDto(lang),

            ImperativeMs = imperative?.MS?.ToDto(lang),
            ImperativeFs = imperative?.FS?.ToDto(lang),
            ImperativeMp = imperative?.MP?.ToDto(lang),
            ExtraInfo = verb.ExtraInfo,
            Gizras = [..verb.Gizras],
            VerbModels = [.. verb.VerbModels],
            Tags = [..verb.Tags]
        };
        return result;
    }

    public static VerbInfo ToVerbInfo(this Verb verb, Language lang = Language.Russian)
    {
        var result = new VerbInfo(
            verb.Id,
            verb.Infinitive.Hebrew,
            verb.Infinitive.HebrewNikkud,
            verb.Binyan,
            verb.Shoresh.WithDots,
            verb.Translations.Select(tr => tr.GetWithAuxillary()),
            verb.Gizras,
            verb.VerbModels,
            verb.Tags);
        return result;
    }
}

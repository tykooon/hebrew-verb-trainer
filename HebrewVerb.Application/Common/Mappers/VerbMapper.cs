using HebrewVerb.Application.Common.Enums;
using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class VerbMapper
{
    public static VerbDto ToDto(this Verb verb, AppLanguage lang,
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
            Binyan = lang == AppLanguage.Russian ? verb.Binyan.NameRussian : verb.Binyan.Name,
            Shoresh = shoresh?.WithDots ?? string.Empty,
            Translate = verb.Translation?.Get((Lang)lang) ?? string.Empty,
            LangId = lang == AppLanguage.Russian ? "ru" : "en",
            PresentMs = present?.MS?.ToDto(lang),
            PresentFs = present?.FS?.ToDto(lang),
            PresentMp = present?.MP?.ToDto(lang),
            PresentFp = present?.FP?.ToDto(lang),

            PastMs1 = past?.MS1?.ToDto(lang),
            PastMp1 = past?.MP1?.ToDto(lang),
            PastMs2 = past?.MS2?.ToDto(lang),
            PastFs2 = past?.FS2?.ToDto(lang),
            PastMp2 = past?.MP2?.ToDto(lang),
            PastFp2 = past?.FP2?.ToDto(lang),
            PastMs3 = past?.MS3?.ToDto(lang),
            PastFs3 = past?.FS3?.ToDto(lang),
            PastMp3 = past?.MP3?.ToDto(lang),

            FutureMs1 = future?.MS1?.ToDto(lang),
            FutureMp1 = future?.MP1?.ToDto(lang),
            FutureMs2 = future?.MS2?.ToDto(lang),
            FutureFs2 = future?.FS2?.ToDto(lang),
            FutureMp2 = future?.MP2?.ToDto(lang),
            FutureMs3 = future?.MS3?.ToDto(lang),
            FutureMp3 = future?.MP3?.ToDto(lang),

            ImperativeMs = imperative?.MS?.ToDto(lang),
            ImperativeFs = imperative?.FS?.ToDto(lang),
            ImperativeMp = imperative?.MP?.ToDto(lang),
            UseFrequency = (int)verb.UseFrequency,
            ExtraInfo = verb.ExtraInfo,
            IsArchaic = verb.IsArchaic,
            IsLiterary = verb.IsLiterary,
            IsSlang = verb.IsSlang,
            VerbModels = verb.VerbModels.Select(vm => vm.Name).ToList()
        };
        return result;
    }

    public static VerbInfo ToVerbInfo(this Verb verb, AppLanguage lang = AppLanguage.Russian)
    {
        var result = new VerbInfo(
            verb.Id,
            verb.Infinitive.Hebrew,
            lang == AppLanguage.Russian? verb.Binyan.NameRussian : verb.Binyan.Name,
            verb.Translation?.Get((Lang)lang) ?? "",
            verb.Shoresh.Gizras.Select(g => g.Name),
            verb.VerbModels.Select(vm => vm.Name));
        return result;
    }
}

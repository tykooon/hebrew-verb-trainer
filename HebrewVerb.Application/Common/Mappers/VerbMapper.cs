using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HebrewVerb.Application.Common.Mappers;

public static class VerbMapper
{
    public static VerbDto ToDto(this Verb verb, Lang lang,
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
            Binyan = lang == Lang.Russian ? verb.Binyan.NameRussian : verb.Binyan.Name,
            Shoresh = shoresh?.WithDots ?? string.Empty,
            Translate = verb.Translate ?? string.Empty,
            LangId = lang == Lang.Russian ? "ru" : "en",
            PresentMs = present?.MS?.ToDto(lang) ?? WordFormDto.Default,
            PresentFs = present?.FS?.ToDto(lang) ?? WordFormDto.Default,
            PresentMp = present?.MP?.ToDto(lang) ?? WordFormDto.Default,
            PresentFp = present?.FP?.ToDto(lang) ?? WordFormDto.Default,

            PastMs1 = past?.MS1?.ToDto(lang) ?? WordFormDto.Default,
            PastMp1 = past?.MP1?.ToDto(lang) ?? WordFormDto.Default,
            PastMs2 = past?.MS2?.ToDto(lang) ?? WordFormDto.Default,
            PastFs2 = past?.FS2?.ToDto(lang) ?? WordFormDto.Default,
            PastMp2 = past?.MP2?.ToDto(lang) ?? WordFormDto.Default,
            PastFp2 = past?.FP2?.ToDto(lang) ?? WordFormDto.Default,
            PastMs3 = past?.MS3?.ToDto(lang) ?? WordFormDto.Default,
            PastFs3 = past?.FS3?.ToDto(lang) ?? WordFormDto.Default,
            PastMp3 = past?.MP3?.ToDto(lang) ?? WordFormDto.Default,

            FutureMs1 = future?.MS1?.ToDto(lang) ?? WordFormDto.Default,
            FutureMp1 = future?.MP1?.ToDto(lang) ?? WordFormDto.Default,
            FutureMs2 = future?.MS2?.ToDto(lang) ?? WordFormDto.Default,
            FutureFs2 = future?.FS2?.ToDto(lang) ?? WordFormDto.Default,
            FutureMp2 = future?.MP2?.ToDto(lang) ?? WordFormDto.Default,
            FutureMs3 = future?.MS3?.ToDto(lang) ?? WordFormDto.Default,
            FutureMp3 = future?.MP3?.ToDto(lang) ?? WordFormDto.Default,

            ImperativeMs = imperative?.MS?.ToDto(lang) ?? WordFormDto.Default,
            ImperativeFs = imperative?.FS?.ToDto(lang) ?? WordFormDto.Default,
            ImperativeMp = imperative?.MP?.ToDto(lang) ?? WordFormDto.Default,
            UseFrequency = (int)verb.UseFrequency,
            ExtraInfo = verb.ExtraInfo,
            IsArchaic = verb.IsArchaic,
            IsLiterary = verb.IsLiterary,
            IsSlang = verb.IsSlang,
            VerbModels = verb.VerbModels.Select(vm => vm.Name).ToList()
        };
        return result;
    }
}

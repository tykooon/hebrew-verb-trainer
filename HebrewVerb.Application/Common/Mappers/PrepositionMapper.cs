using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class PrepositionMapper
{
    public static PrepositionDto ToDto(this Preposition prep, Language lang)
    {
        var result = new PrepositionDto()
        {
            Id = prep.Id,
            BaseForm = prep.BaseForm.ToDto(lang),
            TranslationRus = prep.TranslateRus,
            TranslationEng = prep.TranslateEng,
            LangId = (int)lang,

            MS1 = prep.MS1.ToDto(lang),
            MP1 = prep.MP1.ToDto(lang),
            MS2 = prep.MS2.ToDto(lang),
            FS2 = prep.FS2.ToDto(lang),
            MP2 = prep.MP2.ToDto(lang),
            FP2 = prep.FP2.ToDto(lang),
            MS3 = prep.MS3.ToDto(lang),
            FS3 = prep.FS3.ToDto(lang),
            MP3 = prep.MP3.ToDto(lang),
            FP3 = prep.FP3.ToDto(lang),

        };
        return result;
    }

    //public static VerbDto ToDtoWithUpdate(this Verb verb, Language lang,
    //    Shoresh? shoresh = null,
    //    Past? past = null,
    //    Present? present = null,
    //    Future? future = null,
    //    Imperative? imperative = null)
    //{
    //    var result = new VerbDto()
    //    {
    //        Id = verb.Id,
    //        Infinitive = verb.Infinitive.ToDto(lang),
    //        Binyan = verb.Binyan.ToString(lang),
    //        Shoresh = shoresh?.WithDots ?? string.Empty,
    //        Translation = verb.TranslationFull(lang),
    //        LangId = (int)lang,
    //        PresentMs = present?.MS.ToDto(lang),
    //        PresentFs = present?.FS.ToDto(lang),
    //        PresentMp = present?.MP.ToDto(lang),
    //        PresentFp = present?.FP.ToDto(lang),

    //        PastMs1 = past?.MS1.ToDto(lang),
    //        PastMp1 = past?.MP1.ToDto(lang),
    //        PastMs2 = past?.MS2.ToDto(lang),
    //        PastFs2 = past?.FS2.ToDto(lang),
    //        PastMp2 = past?.MP2.ToDto(lang),
    //        PastFp2 = past?.FP2.ToDto(lang),
    //        PastMs3 = past?.MS3.ToDto(lang),
    //        PastFs3 = past?.FS3.ToDto(lang),
    //        PastMp3 = past?.MP3.ToDto(lang),

    //        FutureMs1 = future?.MS1.ToDto(lang),
    //        FutureMp1 = future?.MP1.ToDto(lang),
    //        FutureMs2 = future?.MS2.ToDto(lang),
    //        FutureFs2 = future?.FS2.ToDto(lang),
    //        FutureMp2 = future?.MP2.ToDto(lang),
    //        FutureMs3 = future?.MS3.ToDto(lang),
    //        FutureMp3 = future?.MP3.ToDto(lang),

    //        ImperativeMs = imperative?.MS?.ToDto(lang),
    //        ImperativeFs = imperative?.FS?.ToDto(lang),
    //        ImperativeMp = imperative?.MP?.ToDto(lang),
    //        ExtraInfo = verb.ExtraInfo,
    //        Gizras = [..verb.Gizras],
    //        VerbModels = [.. verb.VerbModels],
    //        Tags = [..verb.Tags]
    //    };
    //    return result;
    //}

    public static PrepositionInfo ToInfo(this Preposition prep, Language lang = Language.Russian)
    {
        var result = new PrepositionInfo(
            prep.Id,
            prep.BaseForm.Hebrew,
            prep.BaseForm.HebrewNikkud,
            prep.TranslateRus,
            prep.TranslateEng);
        return result;
    }
}

using Ardalis.Result;
using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.Domain.Enums;
using HtmlAgilityPack;

using static HebrewVerb.Application.Common.Constants.PealimVerbFormIds;

namespace HebrewVerb.Application.Common.Helpers;

public static class VerbParser
{
    public static Result<VerbDto> FromUri(string url, bool passive = false)
    {
        var html = new HtmlWeb();

        var document = html.Load(url);

        if (document == null)
        {
            return Result.Error($"No data retrieved from url: {url}");
        }

        var verb = document.ParseToVerb(passive);

        return Result.Success(verb);
    }


    internal static VerbDto ParseToVerb(this HtmlDocument doc, bool passive = false)
    {

        #region Infinitive 

        var infinitive = doc.GetWordFormDto(Infinitive);

        #endregion Infinitive

        #region Binyan

        var binyanRu = doc.GetBinyan();
        var binyan = binyanRu switch
        {
            "ПААЛЬ" => "Paal",
            "ПИЭЛЬ" => "Piel",
            "НИФЪАЛЬ" => "Nifal",
            "hИФЪИЛЬ" => "Hifil",
            "hИТПАЭЛЬ" => "Hitpael",
            "hУФЪАЛЬ" => "Hufal",
            "ПУАЛЬ" => "Pual",
            _ => "Undefined",
        };

        if (passive && (binyan == "Piel" || binyan == "Hifil"))
        {
            binyan = binyan == "Piel" ? "Pual" : "Hufal";
        }
        else
        {
            passive = false;
        }

        #endregion Binyan

        var verbResult = new VerbDto()
        {
            Infinitive = infinitive,
            Binyan = binyan,
            LangId = doc.DocumentNode.SelectSingleNode("/html").GetAttributeValue("lang", "ru")
        };

        #region Shoresh

        var shoreshStr = doc.GetShoresh() ?? "";
        shoreshStr = string.Concat(shoreshStr.Where(c => c != ' ' && c != '-'));
        verbResult.Shoresh = shoreshStr;

        #endregion Shoresh

        #region Past

        if (!passive)
        {
            verbResult.PastMs1 = doc.GetWordFormDto(ActivePastId["ms1"]);
            verbResult.PastMp1 = doc.GetWordFormDto(ActivePastId["mp1"]);
            verbResult.PastMs2 = doc.GetWordFormDto(ActivePastId["ms2"]);
            verbResult.PastFs2 = doc.GetWordFormDto(ActivePastId["fs2"]);
            verbResult.PastMp2 = doc.GetWordFormDto(ActivePastId["mp2"]);
            verbResult.PastFp2 = doc.GetWordFormDto(ActivePastId["fp2"]);
            verbResult.PastMs3 = doc.GetWordFormDto(ActivePastId["ms3"]);
            verbResult.PastFs3 = doc.GetWordFormDto(ActivePastId["fs3"]);
            verbResult.PastMp3 = doc.GetWordFormDto(ActivePastId["mp3"]);
        }
        else
        {
            verbResult.PastMs1 = doc.GetWordFormDto(PassivePastId["ms1"]);
            verbResult.PastMp1 = doc.GetWordFormDto(PassivePastId["mp1"]);
            verbResult.PastMs2 = doc.GetWordFormDto(PassivePastId["ms2"]);
            verbResult.PastFs2 = doc.GetWordFormDto(PassivePastId["fs2"]);
            verbResult.PastMp2 = doc.GetWordFormDto(PassivePastId["mp2"]);
            verbResult.PastFp2 = doc.GetWordFormDto(PassivePastId["fp2"]);
            verbResult.PastMs3 = doc.GetWordFormDto(PassivePastId["ms3"]);
            verbResult.PastFs3 = doc.GetWordFormDto(PassivePastId["fs3"]);
            verbResult.PastMp3 = doc.GetWordFormDto(PassivePastId["mp3"]);
        }

        #endregion Past

        #region Present

        if (!passive)
        {
            verbResult.PresentMs = doc.GetWordFormDto(ActivePresentId["ms"]);
            verbResult.PresentFs = doc.GetWordFormDto(ActivePresentId["fs"]);
            verbResult.PresentMp = doc.GetWordFormDto(ActivePresentId["mp"]);
            verbResult.PresentFp = doc.GetWordFormDto(ActivePresentId["fp"]);        }
        else
        {
            verbResult.PresentMs = doc.GetWordFormDto(PassivePresentId["ms"]);
            verbResult.PresentFs = doc.GetWordFormDto(PassivePresentId["fs"]);
            verbResult.PresentMp = doc.GetWordFormDto(PassivePresentId["mp"]);
            verbResult.PresentFp = doc.GetWordFormDto(PassivePresentId["fp"]);
        }

        #endregion Present

        #region Future

        if (!passive)
        {
            verbResult.FutureMs1 = doc.GetWordFormDto(ActiveFutureId["ms1"]);
            verbResult.FutureMp1 = doc.GetWordFormDto(ActiveFutureId["mp1"]);
            verbResult.FutureMs2 = doc.GetWordFormDto(ActiveFutureId["ms2"]);
            verbResult.FutureFs2 = doc.GetWordFormDto(ActiveFutureId["fs2"]);
            verbResult.FutureMp2 = doc.GetWordFormDto(ActiveFutureId["mp2"]);
            verbResult.FutureMs3 = doc.GetWordFormDto(ActiveFutureId["ms3"]);
            verbResult.FutureMp3 = doc.GetWordFormDto(ActiveFutureId["mp3"]);
        }
        else
        {
            verbResult.FutureMs1 = doc.GetWordFormDto(PassiveFutureId["ms1"]);
            verbResult.FutureMp1 = doc.GetWordFormDto(PassiveFutureId["mp1"]);
            verbResult.FutureMs2 = doc.GetWordFormDto(PassiveFutureId["ms2"]);
            verbResult.FutureFs2 = doc.GetWordFormDto(PassiveFutureId["fs2"]);
            verbResult.FutureMp2 = doc.GetWordFormDto(PassiveFutureId["mp2"]);
            verbResult.FutureMs3 = doc.GetWordFormDto(PassiveFutureId["ms3"]);
            verbResult.FutureMp3 = doc.GetWordFormDto(PassiveFutureId["mp3"]);
        }

        #endregion Future

        #region Imperative

        if (!passive)
        {
            verbResult.ImperativeMs = doc.GetWordFormDto(ActivePresentId["ms"]);
            verbResult.ImperativeFs = doc.GetWordFormDto(ActivePresentId["fs"]);
            verbResult.ImperativeMp = doc.GetWordFormDto(ActivePresentId["mp"]);
        }

        #endregion Imperative

        verbResult.Translate = doc.GetTranslation();

        return verbResult;
    }

    private static (string, int) ExtractStress(string str)
    {
        var stressIndex = str.IndexOf('<');
        if (stressIndex < 0)
        {
            return (str, -1);
        }
        var chunk1 = str[..stressIndex];
        var chunk2 = str[stressIndex+3];
        var chunk3 = str[(stressIndex+8)..];

        return (String.Concat(chunk1, chunk2, chunk3), stressIndex);
    }

    private static WordFormDto GetWordFormDto(this HtmlDocument doc, string idStr, Lang lang = Lang.Russian)
    {
        var hebrewStr = doc.GetVerbForm(idStr) ?? "";
        var translateStr = doc.GetVerbFormTranscript(idStr) ?? "";
        (translateStr, int ind) = ExtractStress(translateStr);

        //WordForm result = lang switch
        //{
        //    Lang.Russian => new(hebrewStr, hebrewStr, translateStr, ind, "", 0),
        //    Lang.English => new(hebrewStr, hebrewStr, "", 0, translateStr, ind),
        //    _ => new(hebrewStr, hebrewStr),
        //};

        return new(hebrewStr, translateStr, ind);
    }
}

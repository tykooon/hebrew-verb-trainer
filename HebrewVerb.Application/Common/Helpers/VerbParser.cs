using Ardalis.Result;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using HtmlAgilityPack;

using static HebrewVerb.Application.Common.Constants.PealimVerbFormIds;

namespace HebrewVerb.Application.Common.Helpers;

public static class VerbParser
{
    public static async Task<Result<VerbDto>> FromUri(string url, bool passive = false)
    {
        var html = new HtmlWeb();

        var document = await html.LoadFromWebAsync(url);

        if (document == null)
        {
            return Result.Error($"No data retrieved from url: {url}");
        }

        if (!document.CheckMetaData("author", "pealim.com"))
        {
            return Result.Error($"Unable to parse data from this url: {url}");
        }

        var docLang = document.GetLangAttribute();
        if (!ParseHelpers.TryGetLanguage(docLang, out var lang))
        {
            return Result.Error($"Unknown language of html. Unable to parse data");
        }

        var verb = document.ParseToVerb(lang, passive);

        return Result.Success(verb);
    }


    internal static VerbDto ParseToVerb(this HtmlDocument doc,
        Language lang = Language.Russian, bool passive = false)
    {
        #region Infinitive 

        var infinitive = doc.GetWordFormDto(Infinitive);

        #endregion Infinitive

        #region Binyan

        var binyanLocal = doc.GetInfo(ParseHelpers.Binyan);
        var binyan = binyanLocal switch
        {
            "ПААЛЬ" or "PA'AL" or "פָּעַל" => "Paal",
            "ПИЭЛЬ" or "PI'EL" or "פִּעֵל" => "Piel",
            "НИФЪАЛЬ" or "NIF'AL" or "נִפְעַל" => "Nifal",
            "hИФЪИЛЬ" or "HIF'IL" or "הִפְעִיל" => "Hifil",
            "hИТПАЭЛЬ" or "HITPA'EL" or "הִתְפַּעֵל" => "Hitpael",
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
            LangId = (int)lang
        };

        #region Shoresh

        var shoreshStr = doc.GetInfo(ParseHelpers.Shoresh).RemoveNonHebrew(false);
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
            verbResult.ImperativeMs = doc.GetWordFormDto(ImperativeId["ms"]);
            verbResult.ImperativeFs = doc.GetWordFormDto(ImperativeId["fs"]);
            verbResult.ImperativeMp = doc.GetWordFormDto(ImperativeId["mp"]);
        }

        #endregion Imperative

        verbResult.Translation = doc.GetInfo(ParseHelpers.Translation);

        return verbResult;
    }

    internal static WordFormDto GetWordFormDto(this HtmlDocument doc, string idStr)
    {
        var hebrewStr = doc.GetVerbForm(idStr, false);
        var hebrewNikkudStr = doc.GetVerbForm(idStr, true);
        var transcriptStr = doc.GetVerbFormTranscript(idStr);
        (transcriptStr, int ind) = ParseHelpers.ExtractStress(transcriptStr);
        if (string.IsNullOrWhiteSpace(hebrewStr))
        {
            hebrewStr = hebrewNikkudStr.RemoveNonHebrew(nikkudAllowed: false);
        }

        return new(hebrewStr, hebrewNikkudStr, transcriptStr, ind);
    }

    internal static bool CheckMetaData(this HtmlDocument htmlDocument, string name, string content)
    {
        var metaNode = htmlDocument.DocumentNode.SelectSingleNode($"//meta[@name='{name}']");
        var metaContent = metaNode?.GetAttributeValue("content", null);

        return metaContent == content;
    }
}

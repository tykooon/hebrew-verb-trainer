using Ardalis.Result;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using HtmlAgilityPack;
using static HebrewVerb.Application.Common.Constants.PealimPrepositionsFormIds;

namespace HebrewVerb.Application.Common.Helpers;

internal static class PrepositionParser
{
    public static async Task<Result<PrepositionDto>> FromUri(string url)
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

        var preposition = document.ParseToPreposition(lang);

        return preposition != null ? Result.Success(preposition) : Result.Error("Unsuccessful parsing...");
    }

    internal static PrepositionDto? ParseToPreposition(this HtmlDocument doc,
        Language lang = Language.Russian)
    {
        #region BaseForm 

        var baseForm = doc.GetBaseForm(lang);
        if (baseForm == null)
        {
            return null;
        }

        #endregion BaseForm

        var prepResult = new PrepositionDto()
        {
            BaseForm = baseForm,
            LangId = (int)lang
        };

        #region ConjugateForm

        prepResult.MS1 = doc.GetWordFormDto(PrepFormId["ms1"]);
        prepResult.MP1 = doc.GetWordFormDto(PrepFormId["mp1"]);
        prepResult.MS2 = doc.GetWordFormDto(PrepFormId["ms2"]);
        prepResult.FS2 = doc.GetWordFormDto(PrepFormId["fs2"]);
        prepResult.MP2 = doc.GetWordFormDto(PrepFormId["mp2"]);
        prepResult.FP2 = doc.GetWordFormDto(PrepFormId["fp2"]);
        prepResult.MS3 = doc.GetWordFormDto(PrepFormId["ms3"]);
        prepResult.FS3 = doc.GetWordFormDto(PrepFormId["fs3"]);
        prepResult.MP3 = doc.GetWordFormDto(PrepFormId["mp3"]);
        prepResult.FP3 = doc.GetWordFormDto(PrepFormId["fp3"]);

        #endregion ConjugateForm

        switch (lang)
        {
            case Language.Russian:
                prepResult.TranslationRus = doc.GetInfo(ParseHelpers.Translation);
                break;
            case Language.English:
                prepResult.TranslationEng = doc.GetInfo(ParseHelpers.Translation);
                break;
        }

        return prepResult;
    }

    internal static WordFormDto? GetBaseForm(this HtmlDocument doc, Language language = Language.Russian)
    {
        var xpath = language switch
        {                               // At now it's equivalent for all languages
            _ => "//h3/following::div[@id=\"b\"]",
        };

        WordFormDto? result = null;
        var node = doc.DocumentNode.SelectNodes(xpath)?.First();
        if (node == null)
        {
            return result;
        }

        var heb = node.SelectSingleNode("//span[@class=\"menukad\"]")?.InnerText;
        if (heb == null)
        {
            return result;
        }

        string transcript = node.SelectSingleNode("//div[@class=\"transcription\"]")?.InnerHtml ?? "";
        (transcript, int ind) = ParseHelpers.ExtractStress(transcript);

        result = new WordFormDto(
            heb.RemoveNonHebrew(false),
            heb.RemoveNonHebrew(true),
            transcript,
            ind);
        return result;
    }

}

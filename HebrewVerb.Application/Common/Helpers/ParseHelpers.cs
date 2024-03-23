using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using HtmlAgilityPack;

namespace HebrewVerb.Application.Common.Helpers;

internal static class ParseHelpers
{
    internal const string Undefined = nameof(Undefined);
    internal const string Shoresh = nameof(Shoresh);
    internal const string Translation = nameof(Translation);
    internal const string Binyan = nameof(Binyan);

    internal static bool TryGetLanguage(string lang, out Language res)
    {
        return lang.ToLower() switch
        {
            "ru" => (res = Language.Russian) == res,
            "en" => (res = Language.English) == res,
            "he" => (res = Language.Hebrew) == res,
            "es" => (res = Language.Spanish) == res,
            _ => (res = Language.English) != res
        };
    }

    internal static string GetLangAttribute(this HtmlDocument doc)
    {
        var lang = doc.DocumentNode.SelectSingleNode("//html").Attributes["lang"].Value;
        return lang;
    }

    internal static string GetInfo(this HtmlDocument doc, string InfoName, Language lang = Language.Russian)
    {
        var xpath = lang switch
        {   // At now it's equivalent for all languages
            _ => InfoName switch
            {
                Binyan => "//h2/following::p/b",
                Shoresh => "//h2/following::a",
                Translation => "//h3/following::div",
                _ => ""
            }
        };
        return doc.ExtractFromNode(xpath);
    }

    internal static string GetVerbForm(this HtmlDocument doc, string form, bool includeNikkud = true, Language lang = Language.Russian)
    {
        var classname = includeNikkud ? "menukad" : "chaser";
        var xpath = lang switch
        {
            _ => string.Format("//*[@id=\"{0}\"]//span[@class='{1}']", form, classname)
        };
        return doc.ExtractFromNode(xpath).RemoveNonHebrew(includeNikkud);
    }

    internal static string GetVerbFormTranscript(this HtmlDocument doc, string form, Language lang = Language.Russian)
    {
        var xpath = lang switch
        {
            // At now it's equivalent for all languages
            _ => form switch
            {
                "PERF-2mp" or "PERF-2fp" or
                "IMPF-2fp" or "IMPF-3fp" or "IMP-2fp" or
                "passive-PERF-2mp" or "passive-PERF-2fp" or
                "passive-IMPF-2fp" or "passive-IMPF-3fp" or
                "passive-IMP-2fp" => "//*[@id=\"" + form + "\"]/div[@class='aux-forms hidden']//span[@class='transcription']",
                _ => "//*[@id=\"" + form + "\"]//div[@class='transcription']"
            }
        };
        return doc.ExtractFromNode(xpath, true);
    }

    private static string ExtractFromNode(this HtmlDocument doc, string xpath, bool withHtml = false)
    {
        var node = doc.DocumentNode.SelectNodes(xpath)?.FirstOrDefault();
        return node != null
            ? (withHtml ? node.InnerHtml : node.InnerText)
            : Undefined;
    }

    internal static HashSet<char> HebrewAlphabet =
        ['א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'כ', 'ך', 'ל', 'מ', 'ם',
            'נ', 'ן', 'ס', 'ע', 'פ', 'ף', 'צ', 'ץ', 'ק', 'ר', 'ש', 'ת', '\'', '־'];

    internal static HashSet<char> HebrewNikkud =
    ['ְ', 'ֱ', 'ֲ', 'ֳ', 'ִ', 'ֵ', 'ֶ', 'ַ', 'ָ', 'ׂ', 'ׁ', 'ֹ', 'ּ', 'ֻ'];

    public static string RemoveNonHebrew(this string word, bool nikkudAllowed = true)
    {
        char[] buffer = new char[word.Length];
        int index = 0;
        foreach (char c in word)
        {
            if (HebrewAlphabet.Contains(c) || (nikkudAllowed && HebrewNikkud.Contains(c)))
            {
                buffer[index] = c;
                index++;
            }
        }
        return new string(buffer, 0, index);
    }

    internal static (string, int) ExtractStress(string str)
    {
        var stressIndex = str.IndexOf('<');
        if (stressIndex < 0)
        {
            return (str, -1);
        }

        var chunk1 = str[..stressIndex];
        var chunk2 = str[stressIndex + 3];
        var chunk3 = str[(stressIndex + 8)..];

        return (string.Concat(chunk1, chunk2, chunk3), stressIndex);
    }
}
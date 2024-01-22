using HtmlAgilityPack;

namespace HebrewVerb.PealimParser;

internal static class Helpers
{
    const string UNDEFINED = "undefined";

    internal static string? GetBinyan(this HtmlDocument doc)
    {
        var node = doc.DocumentNode.SelectNodes("//h2/following::p/b")?.FirstOrDefault();
        return node != null ? node.InnerText : UNDEFINED;
    }

    internal static string? GetShoresh(this HtmlDocument doc)
    {
        var node = doc.DocumentNode.SelectNodes("//h2/following::a")?.FirstOrDefault();
        return node != null ? node.InnerText : UNDEFINED;
    }

    // TODO Clear from possible html. Filter to only hebrew symbols
    internal static string? GetVerbForm(this HtmlDocument doc, string form)
    {
        var node = doc.DocumentNode.SelectNodes("//*[@id=\""+form+"\"]//span[@class='menukad']")?.FirstOrDefault();
        return node != null ? node.InnerText : UNDEFINED;
    }

    internal static string? GetVerbFormTranscript(this HtmlDocument doc, string form)
    {
        var node = form switch
        {
            "PERF-2mp" or 
            "PERF-2fp" or 
            "IMPF-2fp" or
            "IMPF-3fp" or
            "IMP-2fp"  or
            "passive-PERF-2mp" or
            "passive-PERF-2fp" or
            "passive-IMPF-2fp" or
            "passive-IMPF-3fp" or
            "passive-IMP-2fp" => doc.DocumentNode.SelectNodes("//*[@id=\"" + form + "\"]/div[@class='aux-forms hidden']//span[@class='transcription']")?.FirstOrDefault(),
            _ => doc.DocumentNode.SelectNodes("//*[@id=\"" + form + "\"]//div[@class='transcription']")?.FirstOrDefault()
        };
        return node != null ? node.InnerHtml : UNDEFINED;
    }
}

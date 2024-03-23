namespace HebrewVerb.Application.Common.Constants;

// Static class with html-elements ids for parsing pealim
public static class PealimPrepositionsFormIds
{
    //public const string Infinitive = "INF-L";

    public static readonly Dictionary<string, string> PrepFormId = new()
    {
        ["ms1"] = "P-1s",
        ["mp1"] = "P-1p",
        ["ms2"] = "P-2ms",
        ["fs2"] = "P-2fs",
        ["mp2"] = "P-2mp",
        ["fp2"] = "P-2fp",
        ["ms3"] = "P-3ms",
        ["fs3"] = "P-3fs",
        ["mp3"] = "P-3mp",
        ["fp3"] = "P-3fp",
    };
}

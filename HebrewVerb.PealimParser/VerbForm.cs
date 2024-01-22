namespace HebrewVerb.PealimParser;

// Static class with html-elements ids for parsing pealim
public static class VerbForm
{
    public const string Infinitive = "INF-L";

    public static readonly Dictionary<string, string> ActivePresentId = new()
    {
        ["ms"] = "AP-ms",
        ["fs"] = "AP-fs",
        ["mp"] = "AP-mp",
        ["fp"] = "AP-fp"
    };

    public static readonly Dictionary<string, string> PassivePresentId = new()
    {
        ["ms"] = "passive-AP-ms",
        ["fs"] = "passive-AP-fs",
        ["mp"] = "passive-AP-mp",
        ["fp"] = "passive-AP-fp"
    };

    public static readonly Dictionary<string, string> ActivePastId = new()
    {
        ["ms1"] = "PERF-1s",
        ["mp1"] = "PERF-1p",
        ["ms2"] = "PERF-2ms",
        ["fs2"] = "PERF-2fs",
        ["mp2"] = "PERF-2mp",
        ["fp2"] = "PERF-2fp",
        ["ms3"] = "PERF-3ms",
        ["fs3"] = "PERF-3fs",
        ["mp3"] = "PERF-3p",
    };

    public static readonly Dictionary<string, string> PassivePastId = new()
    {
        ["ms1"] = "passive-PERF-1s",
        ["mp1"] = "passive-PERF-1p",
        ["ms2"] = "passive-PERF-2ms",
        ["fs2"] = "passive-PERF-2fs",
        ["mp2"] = "passive-PERF-2mp",
        ["fp2"] = "passive-PERF-2fp",
        ["ms3"] = "passive-PERF-3ms",
        ["fs3"] = "passive-PERF-3fs",
        ["mp3"] = "passive-PERF-3p",
    };

    public static readonly Dictionary<string, string> ImperativeId = new()
    {
        ["ms"] = "IMP-2ms",
        ["fs"] = "IMP-2fs",
        ["mp"] = "IMP-2mp",
        ["fp"] = "IMP-2fp"
    };

    public static readonly Dictionary<string, string> ActiveFutureId = new()
    {
        ["ms1"] = "IMPF-1s",
        ["mp1"] = "IMPF-1p",
        ["ms2"] = "IMPF-2ms",
        ["fs2"] = "IMPF-2fs",
        ["mp2"] = "IMPF-2mp",
        ["fp2"] = "IMPF-2fp",
        ["ms3"] = "IMPF-3ms",
        ["fs3"] = "IMPF-3fs",
        ["mp3"] = "IMPF-3mp",
        ["fp3"] = "IMPF-3fp"
    };

    public static readonly Dictionary<string, string> PassiveFutureId = new()
    {
        ["ms1"] = "passive-IMPF-1s",
        ["mp1"] = "passive-IMPF-1p",
        ["ms2"] = "passive-IMPF-2ms",
        ["fs2"] = "passive-IMPF-2fs",
        ["mp2"] = "passive-IMPF-2mp",
        ["fp2"] = "passive-IMPF-2fp",
        ["ms3"] = "passive-IMPF-3ms",
        ["fs3"] = "passive-IMPF-3fs",
        ["mp3"] = "passive-IMPF-3mp",
        ["fp3"] = "passive-IMPF-3fp"
    };
}

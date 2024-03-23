using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.SharedKernel.Extensions;

public static class HebrewTagFlagExtensions
{
    public static IEnumerable<IHebrewTagFlag> GetTagsFromNames(this IEnumerable<string> list, IEnumerable<IHebrewTagFlag> flagList) =>
        flagList.Where(b => list.Contains(b.Name));

    public static IEnumerable<string> GetTagNames(this IEnumerable<IHebrewTagFlag> flagList, Language lang)
    {
        return flagList.Distinct().Select(f => f.ToString(lang));
    }

    public static string ToFormattedString(this IEnumerable<IHebrewTagFlag> list, Language lang)
    {
        return string.Join(", ", list.GetTagNames(lang));
    }


    public static int GetFlagSum(this IEnumerable<IHebrewTagFlag> list) => 
        list.Distinct().Select(v => v.Flag).Sum();
}
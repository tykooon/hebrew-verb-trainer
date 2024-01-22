using HebrewVerb.Domain.Enums;
using Ardalis.SmartEnum;

namespace HebrewVerb.Application.Common.Mappers;

public static class ZmansMapper
{
    public static IEnumerable<Zman> ToZmanList(this IEnumerable<string> strings)
    {
        foreach (string str in strings)
        {
            if(!string.IsNullOrEmpty(str)
                && SmartEnum<Zman>.TryFromName(str, out Zman zman))
            {
                yield return zman;
            }
        }
    }

    public static IEnumerable<string> ToNameList(this IEnumerable<Zman> zmans) =>
        zmans.Select(x => x.Name);
}

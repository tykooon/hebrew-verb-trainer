using HebrewVerb.Domain.Enums;
using Ardalis.SmartEnum;

namespace HebrewVerb.Application.Common.Mappers;

public static class BinyansMapper
{
    public static IEnumerable<Binyan> ToBinyanList(this IEnumerable<string> strings)
    {
        foreach (string str in strings)
        {
            if(!string.IsNullOrEmpty(str)
                && SmartEnum<Binyan>.TryFromName(str, out Binyan binyan))
            {
                yield return binyan;
            }
        }
    }

    public static IEnumerable<string> ToNameList(this IEnumerable<Binyan> binyans) =>
        binyans.Select(x => x.Name);
}

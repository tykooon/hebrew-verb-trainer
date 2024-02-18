using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.SharedKernel.Extensions;

public static class ZmanExtensions
{
    public static IEnumerable<Zman> GetZmans(this IEnumerable<string> list) =>
        Zman.List.Where(z => list.Contains(z.Name));

    public static IEnumerable<string> GetZmanNames(this IEnumerable<Zman> zmans) =>
        zmans.Distinct().Select(b => b.Name);

}

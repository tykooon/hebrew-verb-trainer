using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.SharedKernel.Extensions;

public static class BinyanExtensions
{

    public static IEnumerable<Binyan> GetBinyans(this byte number) => 
        Binyan.List.Where(b => b.IsIncluded(number));

    public static bool IsIncluded(this Binyan binyan, byte number)
        => (binyan.Value & number) != 0;

    public static IEnumerable<int> GetBinyanIds(this byte number) =>
        Binyan.List.Where(b => b.IsIncluded(number)).Select(b => b.Value);

    public static byte GetBinyanFlags(this IEnumerable<Binyan> binyans) =>
        (byte)binyans.Distinct().Select(b => b.Value).Sum();

    public static IEnumerable<Binyan> GetBinyans(this IEnumerable<string> list) =>
        Binyan.List.Where(b => list.Contains(b.Name));

    public static IEnumerable<string> GetBinyanNames(this IEnumerable<Binyan> binyans) =>
        binyans.Distinct().Select(b => b.Name);

}

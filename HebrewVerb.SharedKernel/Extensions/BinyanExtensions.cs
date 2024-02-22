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

    public static string[] GetNames(this Binyan binyan) =>
        [binyan.Name, binyan.NameRussian, binyan.NameHebrew];

    public static Binyan ToBinyan(this string name) =>
        Binyan.List.FirstOrDefault(binyan => binyan.GetNames().Contains(name)) ?? Binyan.Undefined;

    public static string ToBinyanName(this int value, Language lang = Language.Russian)
    {
        if(!Binyan.TryFromValue(value, out var binyan))
        {
            binyan = Binyan.Undefined; 
        }
        return lang switch
        {
            Language.Russian => binyan.NameRussian,
            Language.Hebrew => binyan.NameHebrew,
            _ => binyan.Name,
        };
    }

    public static Binyan DualBinyan(this Binyan binyan) => binyan.Name switch
    {
        nameof(Binyan.Paal) => Binyan.Nifal,
        nameof(Binyan.Piel) => Binyan.Pual,
        nameof(Binyan.Hifil) => Binyan.Hufal,
        nameof(Binyan.Hitpael) => Binyan.Hitpael,
        nameof(Binyan.Nifal) => Binyan.Paal,
        nameof(Binyan.Pual) => Binyan.Piel,
        nameof(Binyan.Hufal) => Binyan.Hifil,
        _ => Binyan.Undefined
    };
}

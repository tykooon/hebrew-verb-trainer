using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.SharedKernel.Extensions;

public static class BinyanExtensions
{
    public static IEnumerable<Binyan> GetBinyans(this IEnumerable<string> list) =>
        Binyan.List.Where(b => list.Contains(b.Name));

    public static IEnumerable<string> GetBinyanNames(this IEnumerable<Binyan> binyans) =>
        binyans.Distinct().Select(b => b.Name);

    public static string[] GetNames(this Binyan binyan) =>
        [binyan.Name, binyan.NameRussian, binyan.NameHebrew];

    public static Binyan ToBinyan(this string name) =>
        Binyan.List.FirstOrDefault(binyan => binyan.GetNames().Contains(name)) ?? Binyan.Undefined;

    /// <summary>
    ///  Transforms <paramref name="value"/> to the name of binyan in language of given type <paramref name="lang"/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lang"></param>
    /// <returns>Name of <see cref="Binyan"/> in the <see cref="Language"/> <paramref name="lang"/> as <see langword="string"/> </returns>
    public static string ToBinyanName(this int value, Language lang = Language.Russian)
    {
        if(!Binyan.TryFromId(value, out var binyan))
        {
            binyan = Binyan.Undefined; 
        }
        return lang switch
        {
            Language.Russian => binyan!.NameRussian,
            Language.Hebrew => binyan!.NameHebrew,
            _ => binyan!.Name,
        };
    }

    /// <summary>
    /// Inverse active binyan to correspondind passive one and vice versa
    /// </summary>
    /// <param name="binyan"></param>
    /// <returns>Corresponing dual <see cref="Binyan"/> </returns>
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

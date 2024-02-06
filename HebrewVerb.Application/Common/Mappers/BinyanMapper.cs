using HebrewVerb.Domain.Enums;
using Ardalis.SmartEnum;
using HebrewVerb.Application.Models;
using HebrewVerb.Application.Common.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class BinyanMapper
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

    public static Binyan? FromDto(this BinyanDto dto)
    {
        if (dto.Id != null && SmartEnum<Binyan>.TryFromValue(dto.Id.Value, out Binyan result))
        {
            return result;
        }
        if (SmartEnum<Binyan>.TryFromName(dto.Name, out result))
        {
            return result;
        }
        return null;
    }

    public static BinyanDto ToDto(this Binyan binyan, AppLanguage lang = AppLanguage.Russian) =>
        new() {
            Id = binyan.Value,
            Name = binyan.Name,
            NameHebrew = binyan.NameHebrew,
            NameLocal = lang switch {
                AppLanguage.Russian => binyan.NameRussian,
                AppLanguage.Hebrew => binyan.NameHebrew,
                _ => binyan.Name, }
        };

    public static IEnumerable<BinyanDto> ToDtoList(this IEnumerable<Binyan> binyans, AppLanguage lang = AppLanguage.Russian)
    {
        foreach (Binyan binyan in binyans)
        {
            yield return binyan.ToDto(lang);
        }
    }

}

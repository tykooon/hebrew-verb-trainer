using HebrewVerb.Domain.Enums;
using Ardalis.SmartEnum;
using HebrewVerb.Application.Models;
using HebrewVerb.Application.Common.Enums;

namespace HebrewVerb.Application.Common.Mappers;

public static class ZmanMapper
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

    public static Zman? FromDto(this ZmanDto dto)
    {
        if (dto.Id != null && SmartEnum<Zman>.TryFromValue(dto.Id.Value, out Zman result))
        {
            return result;
        }
        if (SmartEnum<Zman>.TryFromName(dto.Name, out result))
        {
            return result;
        }
        return null;
    }

    public static ZmanDto ToDto(this Zman zman, AppLanguage lang = AppLanguage.Russian) =>
        new() {
            Id = zman.Value,
            Name = zman.Name,
            NameHebrew = zman.NameHebrew,
            NameLocal = lang switch {
                AppLanguage.Russian => zman.NameRussian,
                AppLanguage.Hebrew => zman.NameHebrew,
                _ => zman.Name, }
        };

    public static IEnumerable<ZmanDto> ToDtoList(this IEnumerable<Zman> zmans, AppLanguage lang = AppLanguage.Russian)
    {
        foreach (Zman zman in zmans)
        {
            yield return zman.ToDto(lang);
        }
    }

}

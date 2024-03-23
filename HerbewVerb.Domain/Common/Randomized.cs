using HebrewVerb.SharedKernel.Constants;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Domain.Common;

public static class Randomized
{
    private static readonly Random _random = new(DateTime.UtcNow.Millisecond*1000+ DateTime.UtcNow.Microsecond);

    public static bool CoinToss => _random.Next(2) == 1;

    public static Gender Gender => CoinToss ? Gender.Male : Gender.Female;

    public static Number Number => CoinToss ? Number.Single : Number.Plural;

    public static Person Person => _random.Next(3) switch
    {
        0 => Person.First,
        1 => Person.Second,
        2 => Person.Third,
        _ => Person.Impersonal
    };

    public static Zman Zman => _random.Next(3) switch
    {
        0 => Zman.Past,
        1 => Zman.Present,
        2 => Zman.Future,
        _ => Zman.Infinitive,
    };

    public static Guf GufFor(Zman zman) =>
    zman.Name switch
    {
        TenseName.Present => Guf.From(
            CoinToss ? Person.Second : Person.Third, Number, Gender),
        TenseName.Past => _random.Next(9) switch
        {
            < 2 => Guf.From(Person.First, Number, Gender.Male),
            3 => Guf.From(Person.Third, Number.Single, Gender.Male),
            4 => Guf.From(Person.Third, Number.Single, Gender.Female),
            5 => Guf.From(Person.Third, Number.Plural, Gender),
            _ => Guf.From(Person.Second, Number, Gender),
        },
        TenseName.Future => _random.Next(8) switch
        {
            < 2 => Guf.From(Person.First, Number, Gender.Male),
            3 => Guf.From(Person.Second, Number.Plural, Gender),
            4 => Guf.From(Person.Third, Number.Plural, Gender),
            _ => Guf.From(CoinToss ? Person.Second : Person.Third, Number.Single, Gender),
        },
        TenseName.Imperative => _random.Next(3) switch
        {
            < 2 => Guf.From(Person.Second, Number.Single, Gender),
            _ => Guf.From(Person.Second, Number.Plural, Gender),
        },
        _ => Guf.Undefined,
    };

}

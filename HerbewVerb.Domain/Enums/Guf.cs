using Ardalis.SmartEnum;
using Microsoft.VisualBasic;

namespace HebrewVerb.Domain.Enums;

public class Guf : SmartEnum<Guf>
{
    public static readonly Guf Undefined = new(nameof(Undefined), 0b0000, "אין גוף", "Безличное");

    public static readonly Guf S1 = new(nameof(S1), 0b0001, "אני", "Я");
    public static readonly Guf P1 = new(nameof(P1), 0b0011, "אנחנו", "Мы");

    public static readonly Guf MS1 = new(nameof(MS1), 0b0100, "אני (ז')", "Я (м.)");
    public static readonly Guf FS1 = new(nameof(FS1), 0b0101, "אני (נ')", "Я (ж.)");
    public static readonly Guf MP1 = new(nameof(MP1), 0b0110, "אנחנו (ז')", "Мы (м.)");
    public static readonly Guf FP1 = new(nameof(FP1), 0b0111, "אנחנו (נ')", "Мы (ж.)");

    public static readonly Guf MS2 = new(nameof(MS2), 0b1000, "אתה", "Ты (м.)");
    public static readonly Guf FS2 = new(nameof(FS2), 0b1001, "את", "Ты (ж.)");
    public static readonly Guf MP2 = new(nameof(MP2), 0b1010, "אתם", "Вы (м.)");
    public static readonly Guf FP2 = new(nameof(FP2), 0b1011, "אתן", "Вы (ж.)");

    public static readonly Guf MS3 = new(nameof(MS3), 0b1100, "הוא", "Он");
    public static readonly Guf FS3 = new(nameof(FS3), 0b1101, "היא", "Она");
    public static readonly Guf MP3 = new(nameof(MP3), 0b1110, "הם", "Они (м.)");
    public static readonly Guf FP3 = new(nameof(FP3), 0b1111, "הן", "Они (ж.)");


    private Guf(string name, int value, string nameHebrew, string nameRussian) : base(name, value)
    {
        NameHebrew = nameHebrew;
        NameRussian = nameRussian;
    }

    public string NameHebrew { get; }
    public string NameRussian { get; }

    public (Person Person, Number Number, Gender Gender) Details()
    {
        var value = Value;
        return value switch
        {
            0 => (Person.Impersonal, Number.Single, Gender.Male),
            1 => (Person.First, Number.Single, Gender.Male),
            3 => (Person.First, Number.Plural, Gender.Male),
            > 3 and < 16 => (
                (Person)((value >> 2) % 4),
                (Number)((value >> 1) % 2),
                (Gender)(value % 2)),
            _ => throw new InvalidCastException("Found Guf object with incorrect value"),
        };
    }

    public static Guf From(Person person, Number number, Gender gender, bool noGender = false)
    {
        if (person == Person.Impersonal)
        {
            return Undefined;
        }

        if (noGender && person == Person.First)
        {
            return number == Number.Single ? S1 : P1;
        }

        var value = (int)gender + (int)number * 2 + (int)person * 4;
        return FromValue(value);
    }

    public static IEnumerable<Guf> All(bool noGender = true)
    {
        if (noGender)
        {
            yield return S1;
            yield return P1;
        }
        else
        {
            yield return MS1;
            yield return FS1;
            yield return MP1;
            yield return FP1;
        }
        for (var i = 0b1000; i < 0b10000; i++)
        {
            yield return FromValue(i);
        }
    }

    public static IEnumerable<Guf> SecondPersons()
    {
        for (var i = 0b1000; i < 0b01100; i++)
        {
            yield return FromValue(i);
        }
    }
}

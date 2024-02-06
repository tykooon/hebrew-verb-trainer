using Ardalis.SmartEnum;
using HebrewVerb.Domain.Common;

namespace HebrewVerb.Domain.Enums;

public class Zman : SmartEnum<Zman>
{
    public readonly static Zman Infinitive = new(Constants.Infinitive, 0, "שם הפועל", "Неопределённая форма");
    public readonly static Zman Past = new(Constants.Past, 1, "עבר", "Прошедшее");
    public readonly static Zman Present = new(Constants.Present, 2, "הווה", "Настоящее");
    public readonly static Zman Future = new(Constants.Future, 3, "עתיד", "Будущее");
    public readonly static Zman Imperative = new(Constants.Imperative, 4, "ציווי", "Повелительная форма");

    private Zman(string name, int value, string hebrewName, string russianName) : base(name, value)
    {
        NameHebrew = hebrewName;
        NameRussian = russianName;
    }

    public string NameHebrew { get; private set; }
    public string NameRussian { get; private set; }
}

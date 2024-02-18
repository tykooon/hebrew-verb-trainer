using Ardalis.SmartEnum;
using HebrewVerb.SharedKernel.Constants;

namespace HebrewVerb.SharedKernel.Enums;

public class Zman : SmartEnum<Zman>
{
    public readonly static Zman Infinitive = new(TenseName.Infinitive, 0, "שם הפועל", "Неопределённая форма");
    public readonly static Zman Past = new(TenseName.Past, 1, "עבר", "Прошедшее");
    public readonly static Zman Present = new(TenseName.Present, 2, "הווה", "Настоящее");
    public readonly static Zman Future = new(TenseName.Future, 3, "עתיד", "Будущее");
    public readonly static Zman Imperative = new(TenseName.Imperative, 4, "ציווי", "Повелительная форма");

    private Zman(string name, int value, string hebrewName, string russianName) : base(name, value)
    {
        NameHebrew = hebrewName;
        NameRussian = russianName;
    }

    public string NameHebrew { get; private set; }
    public string NameRussian { get; private set; }

    public string ToString(Language language) =>
        language switch
        {
            Language.Russian => NameRussian,
            Language.Hebrew => NameHebrew,
            _ => Name,
        };
}

using Ardalis.SmartEnum;
using HebrewVerb.Domain.Common;

namespace HebrewVerb.Domain.Enums;

public class Zman : SmartEnum<Zman>
{
    public readonly static Zman Infinitive = new(Constants.Infinitive, 0, "שם הפועל");
    public readonly static Zman Past = new(Constants.Past, 1, "עבר");
    public readonly static Zman Present = new(Constants.Present, 2, "הווה");
    public readonly static Zman Future = new(Constants.Future, 3, "עתיד");
    public readonly static Zman Imperative = new(Constants.Imperative, 4, "ציווי");

    private Zman(string name, int value, string hebrewName) : base(name, value)
    {
        HebrewName = hebrewName;
    }

    public string HebrewName { get; private set; }
}

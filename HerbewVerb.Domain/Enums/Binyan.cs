using Ardalis.SmartEnum;

namespace HebrewVerb.Domain.Enums;

public class Binyan : SmartEnum<Binyan>
{
    public static readonly Binyan Undefined = new(nameof(Undefined), 0, "אין בניין", "Не определён");
    public static readonly Binyan Paal = new(nameof(Paal), 1, "פעל", "Пааль");
    public static readonly Binyan Piel = new(nameof(Piel), 2, "פיעל", "Пиэль");
    public static readonly Binyan Hifil = new(nameof(Hifil), 3, "הפעיל", "hифъиль");
    public static readonly Binyan Nifal = new(nameof(Nifal), 4, "נפעל", "Нифъаль");
    public static readonly Binyan Hitpael = new(nameof(Hitpael), 5, "התפעל", "hитпаэль");
    public static readonly Binyan Pual = new(nameof(Pual), 6, "פועל", "Пуаль");
    public static readonly Binyan Hufal = new(nameof(Hufal), 7, "הופעל", "hуфаль");
    
    private Binyan(string name, int value, string nameHebrew, string nameRussian) : base(name, value)
    {
        NameHebrew = nameHebrew;
        NameRussian = nameRussian;
    }

    public string NameHebrew { get; }

    public string NameRussian { get; }




}

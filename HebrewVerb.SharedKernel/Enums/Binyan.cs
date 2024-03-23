using HebrewVerb.SharedKernel.Abstractions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HebrewVerb.SharedKernel.Enums;

[JsonConverter(typeof(BinyanJsonConverter))]
public class Binyan : HebrewTagFlag<Binyan>
{
    private Binyan(string name, int value, string nameHebrew, string nameRussian) :
        base(name, value, nameHebrew, nameRussian)
    {
    }

    public bool IsPassive => Passives.Contains(this);

    #region Static members

    public static readonly Binyan Undefined = new(nameof(Undefined), 0, "אין בניין", "не определён");
    public static readonly Binyan Paal = new(nameof(Paal), 1, "פעל", "пааль");
    public static readonly Binyan Piel = new(nameof(Piel), 2, "פיעל", "пиэль");
    public static readonly Binyan Hifil = new(nameof(Hifil), 3, "הפעיל", "hифъиль");
    public static readonly Binyan Nifal = new(nameof(Nifal), 4, "נפעל", "нифъаль");
    public static readonly Binyan Hitpael = new(nameof(Hitpael), 5, "התפעל", "hитпаэль");
    public static readonly Binyan Pual = new(nameof(Pual), 6, "פועל", "пуаль");
    public static readonly Binyan Hufal = new(nameof(Hufal), 7, "הופעל", "hуфаль");

    public static readonly Binyan[] Passives = [Pual, Hufal, Undefined];

    #endregion
}

public class BinyanJsonConverter : JsonConverter<Binyan>
{
    public override Binyan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Binyan.FromName(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, Binyan value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}

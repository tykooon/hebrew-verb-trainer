using HebrewVerb.SharedKernel.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HebrewVerb.SharedKernel.Enums;

[JsonConverter(typeof(VerbTagJsonConverter))]
public class VerbTag : HebrewTagFlag<VerbTag>, IHebrewTagFlag
{
    private VerbTag(string name, int value, string nameHebrew, string nameRussian) :
        base(name, value, nameHebrew, nameRussian)
    { }

    #region Static members

    public static readonly VerbTag Archaic = new(nameof(Archaic), 1, "", "архаичный");
    public static readonly VerbTag Literary = new(nameof(Literary), 2, "", "литературный");
    public static readonly VerbTag Obsolete = new(nameof(Obsolete), 3, "", "устаревший");
    public static readonly VerbTag Rare = new(nameof(Rare), 4, "", "редкий");
    public static readonly VerbTag Formal = new(nameof(Formal), 5, "", "формальный");
    public static readonly VerbTag Unformal = new(nameof(Unformal), 6, "", "разговорный");
    public static readonly VerbTag Slang = new(nameof(Slang), 7, "", "слэнговый");
    public static readonly VerbTag Tag128 = new(nameof(Tag128), 8, "", "не определён");
    public static readonly VerbTag Tag256 = new(nameof(Tag256), 9, "", "не определён");
    public static readonly VerbTag Tag512 = new(nameof(Tag512), 10, "", "не определён");
    public static readonly VerbTag Tag1024 = new(nameof(Tag1024), 11, "", "не определён");

    #endregion
}

public class VerbTagJsonConverter : JsonConverter<VerbTag>
{
    public override VerbTag? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        VerbTag.FromName(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, VerbTag value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}

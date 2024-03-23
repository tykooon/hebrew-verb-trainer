using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Constants;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HebrewVerb.SharedKernel.Enums;

[JsonConverter(typeof(ZmanJsonConverter))]
public class Zman : HebrewTagFlag<Zman>, IHebrewTagFlag
{
    public readonly static Zman Infinitive = new(TenseName.Infinitive, 0, "שם הפועל", "Неопределённая форма");
    public readonly static Zman Past = new(TenseName.Past, 1, "עבר", "Прошедшее");
    public readonly static Zman Present = new(TenseName.Present, 2, "הווה", "Настоящее");
    public readonly static Zman Future = new(TenseName.Future, 3, "עתיד", "Будущее");
    public readonly static Zman Imperative = new(TenseName.Imperative, 4, "ציווי", "Повелительная форма");

    private Zman(string name, int value, string hebrewName, string russianName) :
        base(name, value, hebrewName, russianName)
    { }

    public static readonly Zman[] MainTenses = [Past, Present, Future];
}

public class ZmanJsonConverter : JsonConverter<Zman>
{
    public override Zman? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Zman.FromName(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, Zman value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}
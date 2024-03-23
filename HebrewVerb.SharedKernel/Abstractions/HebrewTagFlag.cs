using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.TagFlagEnum;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HebrewVerb.SharedKernel.Abstractions;

public abstract class HebrewTagFlag<TEnum> : TagFlagEnum<TEnum>, IHebrewTagFlag
    where TEnum : HebrewTagFlag<TEnum>
{
    private readonly string _nameHebrew;
    private readonly string _nameRussian;
    private readonly string _nameEnglish;

    public string NameHebrew => _nameHebrew;
    public string NameRussian => _nameRussian;
    public string NameEnglish => _nameEnglish;

    public HebrewTagFlag(string name, int id, string nameHebrew, string nameRussian, string nameEnglish = "") :
        base(name, id)
    {
        _nameHebrew = nameHebrew;
        _nameRussian = nameRussian;
        _nameEnglish = string.IsNullOrEmpty(nameEnglish) ? name : nameEnglish;
    }

    public string ToString(Language language) =>
        language switch {
            Language.Russian => NameRussian,
            Language.Hebrew => NameHebrew,
            _ => NameEnglish,
        };
}

//public abstract class HebrewTagFlagJsonConverter<TEnum> : JsonConverter<HebrewTagFlag<TEnum>> where TEnum : HebrewTagFlag<TEnum>
//{
//    public override HebrewTagFlag<TEnum>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
//        HebrewTagFlag<TEnum>.FromName(reader.GetString()!);

//    public override void Write(Utf8JsonWriter writer, HebrewTagFlag<TEnum> value, JsonSerializerOptions options) =>
//        writer.WriteStringValue(value.Name);
//}
using System.Text.Json;

namespace HebrewVerb.Application.Models;

public class Filter
{
    public static readonly JsonSerializerOptions SerializerOptions = new() { };

    public IEnumerable<string> Binyans { get; internal set; } = [];
    public IEnumerable<string> Gizras { get; internal set; } = [];
    public IEnumerable<string> VerbModels { get; internal set; } = [];
    public IEnumerable<string> Tenses { get; internal set; } = [];

    internal Filter() { }

    public static Filter FromJson(string json)
    {
        return JsonSerializer.Deserialize<Filter>(json, SerializerOptions)
            ?? new();
    }

    public string ToJson() =>
        JsonSerializer.Serialize(this, SerializerOptions);

    public static Filter FromParams(
        IEnumerable<string> binyans,
        IEnumerable<string> tenses,
        IEnumerable<string> gizras,
        IEnumerable<string> verbModels)
    {
        return new Filter()
        {
            Binyans = binyans,
            Tenses = tenses,
            Gizras = gizras,
            VerbModels = verbModels
        };
    }

}

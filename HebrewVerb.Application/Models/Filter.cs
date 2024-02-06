using System.Text.Json;

namespace HebrewVerb.Application.Models;

public class Filter
{
    public static readonly Filter Empty = new();
    public static readonly JsonSerializerOptions SerializerOptions = new() { };

    public IEnumerable<string> Binyans { get; set; } = [];
    public IEnumerable<string> Gizras { get; set; } = [];
    public IEnumerable<string> VerbModels { get; set; } = [];
    public IEnumerable<string> Tenses { get; set; } = [];

    public Filter() { }

    public static Filter FromJson(string json) => 
        JsonSerializer.Deserialize<Filter>(json, SerializerOptions) ?? new();

    public string ToJson() =>
        JsonSerializer.Serialize(this, SerializerOptions);

    public static Filter FromParams(
        IEnumerable<string> binyans,
        IEnumerable<string> gizras,
        IEnumerable<string> verbModels,
        IEnumerable<string> tenses)
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

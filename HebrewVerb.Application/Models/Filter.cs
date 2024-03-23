using System.Text.Json;

namespace HebrewVerb.Application.Models;

public class Filter
{
    public static readonly Filter Empty = new();
    public static readonly JsonSerializerOptions SerializerOptions = new() { };

    public HashSet<string> Binyans { get; set; } = [];
    public HashSet<int> Gizras { get; set; } = [];
    public HashSet<int> VerbModels { get; set; } = [];
    public HashSet<int> VerbTags { get; set; } = [];
    public HashSet<string> Zmans { get; set; } = [];
    public int VerbLimit { get; set; } = 10;

    public Filter() { }

    public static Filter FromJson(string json) => 
        JsonSerializer.Deserialize<Filter>(json, SerializerOptions) ?? new();

    public string ToJson() =>
        JsonSerializer.Serialize(this, SerializerOptions);

    public static Filter FromParams(
        IEnumerable<string> binyans,
        IEnumerable<int> gizraIds,
        IEnumerable<int> verbModelIds,
        IEnumerable<int> verbTagIds,
        IEnumerable<string> tenses,
        int verbLimit = 0)
    {
        return new Filter()
        {
            Binyans = binyans.ToHashSet(),
            Zmans = tenses.ToHashSet(),
            Gizras = gizraIds.ToHashSet(),
            VerbModels = verbModelIds.ToHashSet(),
            VerbTags = verbTagIds.ToHashSet(),
            VerbLimit = verbLimit
        };
    }
}

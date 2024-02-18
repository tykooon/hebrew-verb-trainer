using System.Text.Json;

namespace HebrewVerb.Application.Models;

public class Filter
{
    public static readonly Filter Empty = new();
    public static readonly JsonSerializerOptions SerializerOptions = new() { };

    public string FilterName { get; set; } = "";
    public HashSet<string> Binyans { get; set; } = [];
    public HashSet<string> Gizras { get; set; } = [];
    public HashSet<string> VerbModels { get; set; } = [];
    public HashSet<string> Zmans { get; set; } = [];
    public int VerbLimit { get; set; } = 10;

    public Filter() { }

    public static Filter FromJson(string json) => 
        JsonSerializer.Deserialize<Filter>(json, SerializerOptions) ?? new();

    public string ToJson() =>
        JsonSerializer.Serialize(this, SerializerOptions);

    public static Filter FromParams(
        string filterName,
        IEnumerable<string> binyans,
        IEnumerable<string> gizras,
        IEnumerable<string> verbModels,
        IEnumerable<string> tenses,
        int verbLimit)
    {
        return new Filter()
        {
            FilterName = filterName,
            Binyans = binyans.ToHashSet(),
            Zmans = tenses.ToHashSet(),
            Gizras = gizras.ToHashSet(),
            VerbModels = verbModels.ToHashSet(),
            VerbLimit = verbLimit
        };
    }


}

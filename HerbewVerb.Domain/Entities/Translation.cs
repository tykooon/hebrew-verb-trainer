using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text;

namespace HebrewVerb.Domain.Entities;

public class Translation : BaseEntity<int>
{
    public readonly static Translation Default = new();

    public Language Language { get; private set; } = Language.Russian;

    public string Main { get; private set; } = string.Empty;

    public string Auxillare { get; private set; } = string.Empty;

    public ICollection<VerbTag> Tags { get; private set; } = [];

    [ForeignKey("Verb")]
    public int VerbId { get; private set; }

    [JsonIgnore]
    public Verb Verb { get; private set; } = Verb.Empty;

    public ICollection<Preposition> Prepositions { get; } = [];

    public Translation(Language language, string main, string aux = "", params Preposition[] prepositions)
    {
        Language = language;
        Main = main;
        Auxillare = aux;
        AddRangeOfPrepositions(prepositions);
    }

    public Translation() { }

    public void Update(string? main, string? aux, IEnumerable<Preposition>? prepositions)
    {
        if (main != null)
        {
            Main = main;
        }

        if (aux != null)
        {
            Auxillare = aux;
        }
        
        if (prepositions != null)
        {
            Prepositions.Clear();
            AddRangeOfPrepositions([..prepositions]);
        }
    }

    public void UpdateTags(params VerbTag[] verbTags)
    {
        Tags.Clear();        
        foreach (var tag in verbTags)
        {
            Tags.Add(tag);
        }
    }

    private void AddRangeOfPrepositions(params Preposition[] preps)
    {
        foreach (var prep in preps)
        {
            Prepositions.Add(prep);
        }
    }

    public string GetWithAuxillary()
    {
        var result = new StringBuilder(Main);
        switch (string.IsNullOrWhiteSpace(Auxillare), Prepositions.Count == 0)
        {
            case (false, false):
                result.Append($" ({Auxillare} - {string.Join(", ", Prepositions.Select(pr => pr.BaseForm.Hebrew))})");
                break;
            case (false, true):
                result.Append($" ({Auxillare})");
                break;
            case (true, false):
                result.Append($" ({string.Join(", ", Prepositions.Select(pr => pr.BaseForm.Hebrew))})");
                break;
        }
        return result.ToString();

    }
}

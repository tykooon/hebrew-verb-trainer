using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HebrewVerb.Domain.Entities;

public class Gizra : BaseEntity<int>
{
    [Required]
    public string Name { get; private set; } = String.Empty;

    public string? Description { get; private set; }

    public ICollection<Binyan> Binyans { get; private set; } = [];

    [JsonIgnore]
    [InverseProperty("Gizras")]
    public ICollection<Verb> Verbs { get; } = [];

    public Gizra(string name, string description, params Binyan[] binyans)
    {
        Update(name, description, binyans);
    }

    private Gizra() { }

    public void Update(string name, string description, params Binyan[] binyans)
    {
        Name = name;
        Description = description;
        Binyans = binyans;
    }
}

using HebrewVerb.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HebrewVerb.Domain.Entities;

public class Gizra : BaseEntity<int>
{
    [Required]
    public string Name { get; private set; } = String.Empty;

    public string? Description { get; private set; }

    [JsonIgnore]
    [InverseProperty("Gizras")]
    public ICollection<Shoresh> Shoreshes { get; } = [];

    public Gizra(string name, string description)
    {
        Name = name;
        Description = description;
    }

    private Gizra() { }
}

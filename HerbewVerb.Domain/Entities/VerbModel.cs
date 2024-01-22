using HebrewVerb.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HebrewVerb.Domain.Entities;

public class VerbModel : BaseEntity<int>
{
    [Key]
    [Required]
    public string Name { get; private set; } = String.Empty;

    public string? Description { get; private set; }

    [JsonIgnore]
    public ICollection<Verb> Verbs { get; } = [];
}

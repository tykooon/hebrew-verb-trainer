using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HebrewVerb.Domain.Entities;

public class VerbModel : BaseEntity<int>
{
    [Required]
    public string Name { get; private set; } = String.Empty;

    public string? Description { get; private set; }


    public ICollection<Binyan> Binyans { get; private set; } = [];

    [JsonIgnore]
    public ICollection<Verb> Verbs { get; } = [];

    public VerbModel(string name, string description, params Binyan[] binyans) 
    {
        Name = name;
        Description = description;
        Binyans = binyans;
    }

    private VerbModel() { }

}

using HebrewVerb.SharedKernel.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;


namespace HebrewVerb.Domain.Entities;

public class VerbPreposition : BaseEntity<int>
{
    [ForeignKey("Verb")]
    public int VerbId { get; set; }

    [ForeignKey("Preposition")]
    public int PrepositionId { get; set; }

    [ForeignKey("Translation")]
    public int TranslationId { get; set; }

    public Translation Translation { get; set; } = Translation.Default;
}

using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Models;

public record VerbInfo(
    int VerbId,
    string Infinitive,
    string InfinitiveNikkud,
    Binyan Binyan,
    string Shoresh,
    IEnumerable<string> Translations,
    IEnumerable<Gizra> Gizras,
    IEnumerable<VerbModel> VerbModels,
    IEnumerable<VerbTag> Tags
)
{ }

using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Models;

public record TranslationDto(
    int Id,
    Language Language,
    int VerbId,
    string Main,
    string Auxillare,
    IEnumerable<VerbTag> Tags,
    IEnumerable<int> Prepositions)
{ }

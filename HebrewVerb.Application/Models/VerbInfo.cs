namespace HebrewVerb.Application.Models;

public record VerbInfo(
    int VerbId,
    string Infinitive,
    string Binyan,
    string Translate,
    IEnumerable<string> Gizras,
    IEnumerable<string> VerbModels)
{ }

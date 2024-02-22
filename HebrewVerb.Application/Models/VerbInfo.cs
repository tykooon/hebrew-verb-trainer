namespace HebrewVerb.Application.Models;

public record VerbInfo(
    int VerbId,
    string Infinitive,
    int Binyan,
    string Shoresh,
    string Translate,
    IEnumerable<string> Gizras,
    IEnumerable<string> VerbModels,
    bool IsArchaic,
    bool IsLiterary,
    bool IsSlang)
{ }

namespace HebrewVerb.WebApp.Contracts;

public record AddVerbRequest (
    string Url,
    bool IsPassive = false,
    string Source = "pealim");

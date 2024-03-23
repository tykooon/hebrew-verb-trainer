namespace HebrewVerb.WebApp.Contracts;

public record AddPrepositionRequest (
    string Url,
    string Source = "pealim");

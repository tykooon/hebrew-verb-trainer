namespace HebrewVerb.WebApp.Contracts;

public record AddVerbFromUriRequest (string Url, bool IsPassive = false);

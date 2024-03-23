namespace HebrewVerb.WebApp.Contracts;

// Class for binding query params for GET request for verb
public class VerbByIdRequest
{
    public string? Lang { get; set; } = "rus";
    public bool AllForms { get; set; } = false;
}

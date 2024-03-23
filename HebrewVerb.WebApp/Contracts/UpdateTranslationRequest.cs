namespace HebrewVerb.WebApp.Contracts;

public class UpdateTranslationRequest
{
    public int Id { get; set; }
    public string? Main { get; set; }
    public string? Aux { get; set; }
    public List<string>? Tag { get; set; }
    public IEnumerable<int>? PrepositionIds { get; set; }
}

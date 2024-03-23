namespace HebrewVerb.WebApp.Contracts;

public class VerbsByFilterRequest
{
    public string? Lang { get; set; } = "rus";
    public List<int> Id { get; set; } = [];
    public List<string> Binyan { get; set; } = [];
    public List<int> GizraId { get; set; } = [];
    public List<int> ModelId { get; set; } = [];
    public List<int> TagId { get; set; } = [];
}

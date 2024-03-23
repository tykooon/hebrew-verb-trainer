namespace HebrewVerb.WebApp.Contracts;

public class TrainingPrepositionRequest
{
    public string? Lang { get; set; } = "rus";
    public List<int>? Id { get; set; } = [];
    public int Take { get; set; } = 0;
}

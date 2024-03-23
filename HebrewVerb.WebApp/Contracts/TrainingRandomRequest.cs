using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.WebApp.Contracts;

public class TrainingRandomRequest
{
    public string? Lang { get; set; } = "rus";
    public List<int> Id { get; set; } = [];
    public List<string> Binyan { get; set; } = [];
    public List<int> GizraId { get; set; } = [];
    public List<int> ModelId { get; set; } = [];
    public List<int> TagId { get; set; } = [];
    public List<string> Zman { get; set; } = [];
    public int Take { get; set; } = 0;
}

using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.WebApp.Contracts;

public class TrainingSelectedRequest
{
    public string? Lang { get; set; } = "rus";
    public List<int> Id { get; set; } = [];
    public List<string> Zman { get; set; } = [];
}

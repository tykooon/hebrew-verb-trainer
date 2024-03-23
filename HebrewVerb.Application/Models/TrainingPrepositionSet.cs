namespace HebrewVerb.Application.Models;

public class TrainingPrepositionSet
{
    public int MaxLimit { get; set; }
    public IEnumerable<PrepositionFormCard> FormCards { get; set; } = [];
    public Dictionary<int, PrepositionInfo> Prepositions { get; set; } = [];
}

namespace HebrewVerb.Application.Models;

public class TrainingSet
{
    public int MaxLimit { get; set; }
    public Filter Filter { get; set; } = Filter.Empty;
    public IEnumerable<VerbFormCard> FormCards { get; set; } = [];
    public Dictionary<int, VerbInfo> Verbs { get; set; } = [];
}

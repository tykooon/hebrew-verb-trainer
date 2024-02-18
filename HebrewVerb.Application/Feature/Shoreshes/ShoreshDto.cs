namespace HebrewVerb.Application.Feature.Shoreshes;

public class ShoreshDto
{
    public int Id { get; set; }
    public string ShortName { get; set; } = String.Empty;
    public string NameWithDots { get; set; } = String.Empty;
    public IEnumerable<int> VerbIds { get; set; } = [];
}

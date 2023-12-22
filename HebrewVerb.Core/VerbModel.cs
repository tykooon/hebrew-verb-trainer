namespace HebrewVerb.Core;

public class VerbModel : Entity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsGizra { get; set; } = false;

    public List<Shoresh> Shoreshes { get; } = [];
}

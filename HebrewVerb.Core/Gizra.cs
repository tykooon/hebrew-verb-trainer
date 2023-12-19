namespace HebrewVerb.Core;

public class Gizra : Entity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<Shoresh> Shoreshes { get; } = [];
}

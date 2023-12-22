namespace HebrewVerb.Core;

public class Shoresh : Entity<int>
{
    public string? Name { get; set; }

    public List<VerbModel> VerbModels { get; set; } = [];
}

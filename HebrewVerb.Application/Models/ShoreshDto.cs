namespace HebrewVerb.Application.Models;

public class ShoreshDto
{
    public int Id { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string NameWithDots { get; set; } = string.Empty;
    public IEnumerable<int> VerbIds { get; set; } = [];
}

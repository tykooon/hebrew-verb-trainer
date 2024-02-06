namespace HebrewVerb.Application.Models;

public class GizraDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not GizraDto dto) return false;
        if (ReferenceEquals(this, obj)) return true;
        return (Id == dto.Id);
    }
    public override int GetHashCode() => Id.GetHashCode();
}

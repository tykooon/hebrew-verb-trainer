using HebrewVerb.SharedKernel.Enums;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HebrewVerb.Application.Models;

public class GizraDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Gizra Name cannot be empty.")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IEnumerable<string> Binyans { get; set; } = [];

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not GizraDto dto) return false;
        if (ReferenceEquals(this, obj)) return true;
        return (Id == dto.Id);
    }
    public override int GetHashCode() => Id.GetHashCode();
}

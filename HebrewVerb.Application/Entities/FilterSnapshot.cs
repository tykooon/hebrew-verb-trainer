using HebrewVerb.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HebrewVerb.Application.Entities;

public class FilterSnapshot : BaseEntity<int>
{
    public static readonly FilterSnapshot Empty = new();

    public string FilterJson { get; private set; } = String.Empty;

    public string? FilterName { get; private set; }

    [ForeignKey("AppUser")]
    public int AppUserId { get; private set; }

    //[InverseProperty("FilterSnapshot")]
    public AppUser AppUser { get; private set; } = new();
}

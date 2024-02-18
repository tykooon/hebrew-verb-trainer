using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HebrewVerb.Application.Entities;

public class AppFilter : BaseEntity<int>
{
    public static readonly AppFilter Default = new();
    public const string DefaultName = "Default";

    public Filter Filter { get; set; } = Filter.Empty;

    public string FilterName { get; set; } = DefaultName;

    [ForeignKey("AppUser")]
    public int AppUserId { get; private set; }

    [JsonIgnore]
    public AppUser AppUser { get; private set; } = new();
}

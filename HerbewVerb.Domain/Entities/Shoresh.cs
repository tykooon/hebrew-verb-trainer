using Ardalis.GuardClauses;
using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HebrewVerb.Domain.Entities;

public class Shoresh : BaseEntity<int>
{
    public static readonly Shoresh Empty = new();

    public string Short { get; private set; } = "םםם";

    public int Length => Short.Length;

    public char First => Short[0];
    public char Second => Short[1];
    public char Third => Short[2];
    public char? Fourth => Length > 3 ? Short[3] : null;

    public bool IsLong => Fourth != null;

    public string WithDots => $"{First}.{Second}.{Third}." + (IsLong ? $"{Fourth}." : "");

    [JsonIgnore]
    [InverseProperty("Shoresh")]
    public ICollection<Verb> Verbs { get; private set; } = [];

    private Shoresh()
    { }

    public Shoresh(string shortForm)
    {
        Guard.Against.NullOrWhiteSpace(shortForm);
        if (shortForm.Length < 3 || shortForm.Length > 4)
        {
            throw new DomainException($"Wrong Length fo Shoresh {shortForm}");
        }

        Short = shortForm;
    }

    [InverseProperty("Shoreshes")]
    public ICollection<Gizra> Gizras { get; } = [];

}

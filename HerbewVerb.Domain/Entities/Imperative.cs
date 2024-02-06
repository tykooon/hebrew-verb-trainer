using Ardalis.GuardClauses;
using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;

namespace HebrewVerb.Domain.Entities;

public class Imperative : BaseEntity<int>, IConjugation
{
    public static readonly Imperative Empty = new();

    public WordForm? MS { get; private set; }
    public WordForm? FS { get; private set; }
    public WordForm? MP { get; private set; }

    public WordForm? FP => MP;

    private Imperative () {}

    public WordForm? Conjugate(Guf guf)
    {
        Guard.Against.Null(guf);
        var details = guf.Details();
        if (details.Person != Person.Second)
        {
            return null;
        }

        return details.Number == Number.Plural
            ? MP
            : details.Gender == Gender.Male ? MS : FS;
    }

    public Imperative(WordForm? mS, WordForm? fS, WordForm? mP)
    {
        MS = mS;
        FS = fS;
        MP = mP;
    }
}

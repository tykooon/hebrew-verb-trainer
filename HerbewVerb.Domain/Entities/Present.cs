using Ardalis.GuardClauses;
using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;

namespace HebrewVerb.Domain.Entities;

public class Present : BaseEntity<int>, IConjugation
{
    public static readonly Present Empty = new();

    public WordForm MS { get; private set; } = WordForm.Default;
    public WordForm FS { get; private set; } = WordForm.Default;
    public WordForm MP { get; private set; } = WordForm.Default;
    public WordForm FP { get; private set; } = WordForm.Default;

    private Present() { }

    public WordForm? Conjugate(Guf guf)
    {
        Guard.Against.Null(guf);
        var details = guf.Details();
        return details.Number == Number.Plural
            ? details.Gender == Gender.Male ? MP : FP
            : details.Gender == Gender.Male ? MS : FS;
    }

    public Present(WordForm mS, WordForm fS, WordForm mP, WordForm fP)
    {
        MS = mS;
        FS = fS;
        MP = mP;
        FP = fP;
    }
}

using Ardalis.GuardClauses;
using HebrewVerb.Domain.Interfaces;
using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Domain.Entities;

public class Future : BaseEntity<int>, IConjugation
{
    public static readonly Future Empty = new();

    public WordForm MS1 { get; private set; } = WordForm.Default;
    public WordForm MP1 { get; private set; } = WordForm.Default;
    public WordForm MS2 { get; private set; } = WordForm.Default;
    public WordForm FS2 { get; private set; } = WordForm.Default;
    public WordForm MP2 { get; private set; } = WordForm.Default;
    public WordForm MS3 { get; private set; } = WordForm.Default;
    public WordForm MP3 { get; private set; } = WordForm.Default;

    public WordForm FS1 => MS1;
    public WordForm FP1 => MP1;
    public WordForm FP2 => MP2;
    public WordForm FS3 => MS2;
    public WordForm FP3 => MP3;

    private Future() { }

    public WordForm? Conjugate(Guf guf)
    {
        Guard.Against.Null(guf);
        return guf.Details() switch
        {
            (Person.First, Number.Single, Gender.Male) => MS1,
            (Person.First, Number.Single, Gender.Female) => FS1,
            (Person.First, Number.Plural, Gender.Male) => MP1,
            (Person.First, Number.Plural, Gender.Female) => FP1,

            (Person.Second, Number.Single, Gender.Male) => MS2,
            (Person.Second, Number.Single, Gender.Female) => FS2,
            (Person.Second, Number.Plural, Gender.Male) => MP2,
            (Person.Second, Number.Plural, Gender.Female) => FP2,

            (Person.Third, Number.Single, Gender.Male) => MS3,
            (Person.Third, Number.Single, Gender.Female) => FS3,
            (Person.Third, Number.Plural, Gender.Male) => MP3,
            (Person.Third, Number.Plural, Gender.Female) => FP3,

            _ => MP3
        };
    }

    public Future(
        WordForm mS1,
        WordForm mP1,
        WordForm mS2,
        WordForm fS2,
        WordForm mP2,
        WordForm mS3,
        WordForm mP3)
    {
        MS1 = mS1;
        MP1 = mP1;
        MS2 = mS2;
        FS2 = fS2;
        MP2 = mP2;
        MS3 = mS3;
        MP3 = mP3;
    }
}

using Ardalis.GuardClauses;
using HebrewVerb.Domain.Interfaces;
using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Domain.Entities;

public class Past : BaseEntity<int>, IConjugation
{
    public static readonly Past Empty = new();

    public WordForm MS1 { get; set; } = WordForm.Default;
    public WordForm MP1 { get; set; } = WordForm.Default;
    public WordForm MS2 { get; set; } = WordForm.Default;
    public WordForm FS2 { get; set; } = WordForm.Default;
    public WordForm MP2 { get; set; } = WordForm.Default;
    public WordForm FP2 { get; set; } = WordForm.Default;
    public WordForm MS3 { get; set; } = WordForm.Default;
    public WordForm FS3 { get; set; } = WordForm.Default;
    public WordForm MP3 { get; set; } = WordForm.Default;

    public WordForm FS1 => MS1;
    public WordForm FP1 => MP1;
    public WordForm FP3 => MP3;

    private Past() { }

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

    public Past(
        WordForm mS1,
        WordForm mP1,
        WordForm mS2,
        WordForm fS2,
        WordForm mP2,
        WordForm fP2,
        WordForm mS3,
        WordForm fS3,
        WordForm mP3)
    {
        MS1 = mS1;
        MP1 = mP1;
        MS2 = mS2;
        FS2 = fS2;
        MP2 = mP2;
        FP2 = fP2;
        MS3 = mS3;
        FS3 = fS3;
        MP3 = mP3;
    }
}

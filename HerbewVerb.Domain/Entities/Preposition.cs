using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Domain.Entities;

public class Preposition : BaseEntity<int>
{
    public WordForm BaseForm { get; private set; } = WordForm.Default;

    public Translation Translation { get; set; } = Translation.Default;

    public ICollection<Verb> Verbs { get; private set; } = [];

    public WordForm MS1 { get; private set; } = WordForm.Default;
    public WordForm MP1 { get; private set; } = WordForm.Default;
    public WordForm MS2 { get; private set; } = WordForm.Default;
    public WordForm MP2 { get; private set; } = WordForm.Default;
    public WordForm FS2 { get; private set; } = WordForm.Default;
    public WordForm FP2 { get; private set; } = WordForm.Default;
    public WordForm MS3 { get; private set; } = WordForm.Default;
    public WordForm MP3 { get; private set; } = WordForm.Default;
    public WordForm FS3 { get; private set; } = WordForm.Default;
    public WordForm FP3 { get; private set; } = WordForm.Default;

    public WordForm FS1 => MS1;
    public WordForm FP1 => MP1;

    public WordForm GetForm(Guf guf) => guf.Details() switch
    {
        (Person.First, Number.Single, _) => MS1,
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
        _ => BaseForm,
    };

}

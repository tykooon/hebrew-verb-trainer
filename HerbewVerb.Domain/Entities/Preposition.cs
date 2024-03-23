using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;
using System.Text.Json.Serialization;

namespace HebrewVerb.Domain.Entities;

public class Preposition : BaseEntity<int>
{
    public WordForm BaseForm { get; private set; } = WordForm.Default;

    public string TranslateRus { get; set; } = string.Empty;
    public string TranslateEng { get; set; } = string.Empty;

    // Verb translations with this preposition !!!
    [JsonIgnore]
    public ICollection<Translation> Translations { get; private set; } = [];

    public WordForm MS1 { get; set; } = WordForm.Default;
    public WordForm MP1 { get; set; } = WordForm.Default;
    public WordForm MS2 { get; set; } = WordForm.Default;
    public WordForm MP2 { get; set; } = WordForm.Default;
    public WordForm FS2 { get; set; } = WordForm.Default;
    public WordForm FP2 { get; set; } = WordForm.Default;
    public WordForm MS3 { get; set; } = WordForm.Default;
    public WordForm MP3 { get; set; } = WordForm.Default;
    public WordForm FS3 { get; set; } = WordForm.Default;
    public WordForm FP3 { get; set; } = WordForm.Default;

    public WordForm FS1 => MS1;
    public WordForm FP1 => MP1;

    private Preposition()
    { }

    public Preposition(WordForm baseForm)
    {
        BaseForm = baseForm;
    }

    public string Translate(Language language = Language.Russian) =>
        language switch
        {
            Language.Russian => TranslateRus,
            _ => TranslateEng
        };

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

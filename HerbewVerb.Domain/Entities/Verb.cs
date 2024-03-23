using HebrewVerb.Domain.Interfaces;
using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace HebrewVerb.Domain.Entities;

public class Verb : BaseEntity<int>
{
    public static readonly Verb Empty = new();

    #region Properties

    public WordForm Infinitive { get; private set; } = WordForm.Default;

    public Binyan Binyan { get; private set; } = Binyan.Undefined;

    [ForeignKey("Shoresh")]
    public int ShoreshId { get; private set; }

    [InverseProperty("Verbs")]
    public Shoresh Shoresh { get; set; } = Shoresh.Empty;

    [ForeignKey("Present")]
    public int? PresentId { get; private set; }

    public Present? Present { get; set; }

    [ForeignKey("Past")]
    public int? PastId { get; private set; }

    public Past? Past { get; set; }

    [ForeignKey("Future")]
    public int? FutureId { get; private set; }

    public Future? Future { get; set; }

    [ForeignKey("Imperative")]
    public int? ImperativeId { get; private set; }

    public Imperative? Imperative { get; set; }

    public ICollection<VerbTag> Tags { get; set; } = [];

    public Dictionary<string, string> ExtraInfo { get; set; } = [];

    public ICollection<VerbModel> VerbModels { get; set; } = [];

    public ICollection<Gizra> Gizras { get; set; } = [];

    public ICollection<Translation> Translations { get; set; } = [];

    #endregion Properties

    #region Constructors

    private Verb() { }

    public Verb(
        Binyan binyan,
        Shoresh shoresh,
        WordForm infinitive,
        Past? past,
        Present? present,
        Future? future,
        Imperative? imperative,
        IEnumerable<Translation>? translations = null,
        IEnumerable<VerbTag>? tags = null,
        Dictionary<string, string>? extraInfo = null)
    {
        Binyan = binyan;
        Shoresh = shoresh;
        Infinitive = infinitive;
        Past = past;
        Present = present;
        Future = future;
        Imperative = imperative;
        if (translations != null)
        {
            Translations = translations.ToList();
        }

        if (tags != null)
        { 
            Tags = tags.ToList();
        }
        if(extraInfo != null)
        {
            ExtraInfo = extraInfo;
        };
    }

    #endregion Constructors

    public IConjugation? Tense(Zman zman) => zman.Name switch
    {
        TenseName.Past => Past,
        TenseName.Present => Present,
        TenseName.Future => Future,
        TenseName.Imperative => Imperative,
        _ => null
    };

    public WordForm? GetForm(Zman zman, Guf guf) => Tense(zman)?.Conjugate(guf);

    public string TranslationFull(Language lang) =>
        string.Join("; ", Translations.Where(tr => tr.Language == lang).Select(tr => tr.GetWithAuxillary()));

}

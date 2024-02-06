using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;
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
    public int PresentId { get; private set; }

    public Present Present { get; private set; } = Present.Empty;

    [ForeignKey("Past")]
    public int PastId { get; private set; }

    public Past Past { get; private set; } = Past.Empty;

    [ForeignKey("Future")]
    public int FutureId { get; private set; }

    public Future Future { get; private set; } = Future.Empty;

    [ForeignKey("Imperative")]
    public int ImperativeId { get; private set; }

    public Imperative Imperative { get; private set; } = Imperative.Empty;

    [ForeignKey("Translation")]
    public int TranslationId { get; private set; }

    public Translation Translation { get; private set; } = Translation.Default;

    public UseFrequency UseFrequency { get; private set; }

    public string? ExtraInfo { get; private set; }
    public bool IsArchaic { get; private set; } = false;
    public bool IsLiterary { get; private set; } = false;
    public bool IsSlang { get; private set; } = false;

    public ICollection<VerbModel> VerbModels { get; } = [];

    public ICollection<Preposition> Prepositions { get; } = [];

    #endregion Properties

    public IConjugation? Tense(Zman zman) => zman.Name switch
    {
        Constants.Past => Past,
        Constants.Present => Present,
        Constants.Future => Future,
        Constants.Imperative => Imperative,
        _ => null
    };

    public WordForm? GetForm(Zman zman, Guf guf) => Tense(zman)?.Conjugate(guf);

    #region Constructors

    private Verb() { }

    public Verb(
        Binyan binyan,
        Shoresh shoresh,
        WordForm infinitive,
        Past past,
        Present present,
        Future future,
        Imperative imperative,
        Translation translation,
        UseFrequency useFrequency = UseFrequency.Undefined,
        string? extraInfo = null,
        bool isArchaic = false,
        bool isLiterary = false,
        bool isSlang = false)
    {
        Binyan = binyan;
        Shoresh = shoresh;
        Infinitive = infinitive;
        Past = past;
        Present = present;
        Future = future;
        Imperative = imperative;
        UseFrequency = useFrequency;
        Translation = translation;
        ExtraInfo = extraInfo;
        IsArchaic = isArchaic;
        IsLiterary = isLiterary;
        IsSlang = isSlang;
    }

    #endregion Constructors
}

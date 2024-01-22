
namespace HebrewVerb.Application.Feature.Verbs;

public class VerbDto
{
    public int Id { get; set; }
    public WordFormDto? Infinitive { get; set; }
    public string Binyan { get; set; } = "Undefined";
    public string Shoresh { get; set; } = string.Empty;
    public string Translate { get; set; } = string.Empty;
    public string LangId { get; set; } = "ru";

    public WordFormDto PresentMs { get; set; } = WordFormDto.Default;
    public WordFormDto PresentFs { get; set; } = WordFormDto.Default;
    public WordFormDto PresentMp { get; set; } = WordFormDto.Default;
    public WordFormDto PresentFp { get; set; } = WordFormDto.Default;

    public WordFormDto PastMs1 { get; set; } = WordFormDto.Default;
    public WordFormDto PastMp1 { get; set; } = WordFormDto.Default;
    public WordFormDto PastMs2 { get; set; } = WordFormDto.Default;
    public WordFormDto PastFs2 { get; set; } = WordFormDto.Default;
    public WordFormDto PastMp2 { get; set; } = WordFormDto.Default;
    public WordFormDto PastFp2 { get; set; } = WordFormDto.Default;
    public WordFormDto PastMs3 { get; set; } = WordFormDto.Default;
    public WordFormDto PastFs3 { get; set; } = WordFormDto.Default;
    public WordFormDto PastMp3 { get; set; } = WordFormDto.Default;

    public WordFormDto FutureMs1 { get; set; } = WordFormDto.Default;
    public WordFormDto FutureMp1 { get; set; } = WordFormDto.Default;
    public WordFormDto FutureMs2 { get; set; } = WordFormDto.Default;
    public WordFormDto FutureFs2 { get; set; } = WordFormDto.Default;
    public WordFormDto FutureMp2 { get; set; } = WordFormDto.Default;
    public WordFormDto FutureMs3 { get; set; } = WordFormDto.Default;
    public WordFormDto FutureMp3 { get; set; } = WordFormDto.Default;
    public WordFormDto ImperativeMs { get; set; } = WordFormDto.Default;
    public WordFormDto ImperativeFs { get; set; } = WordFormDto.Default;
    public WordFormDto ImperativeMp { get; set; } = WordFormDto.Default;

    public int UseFrequency { get; set; }

    public string? ExtraInfo { get; set; } 
    public bool IsArchaic { get; set; } = false;
    public bool IsLiterary { get; set; } = false;
    public bool IsSlang { get; set; } = false;

    public List<string> VerbModels { get; set; } = [];

    public VerbDto()
    {
    }
}


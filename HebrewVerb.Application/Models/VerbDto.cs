using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Models;

public class VerbDto
{
    public int Id { get; set; }
    public WordFormDto Infinitive { get; set; } = WordFormDto.Default;
    public string Binyan { get; set; } = "Undefined";
    public string Shoresh { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public int LangId { get; set; } = (int)Language.Russian;

    public WordFormDto? PresentMs { get; set; }
    public WordFormDto? PresentFs { get; set; }
    public WordFormDto? PresentMp { get; set; }
    public WordFormDto? PresentFp { get; set; }

    public WordFormDto? PastMs1 { get; set; }
    public WordFormDto? PastMp1 { get; set; }
    public WordFormDto? PastMs2 { get; set; }
    public WordFormDto? PastFs2 { get; set; }
    public WordFormDto? PastMp2 { get; set; }
    public WordFormDto? PastFp2 { get; set; }
    public WordFormDto? PastMs3 { get; set; }
    public WordFormDto? PastFs3 { get; set; }
    public WordFormDto? PastMp3 { get; set; }

    public WordFormDto? FutureMs1 { get; set; }
    public WordFormDto? FutureMp1 { get; set; }
    public WordFormDto? FutureMs2 { get; set; }
    public WordFormDto? FutureFs2 { get; set; }
    public WordFormDto? FutureMp2 { get; set; }
    public WordFormDto? FutureMs3 { get; set; }
    public WordFormDto? FutureMp3 { get; set; }
    public WordFormDto? ImperativeMs { get; set; }
    public WordFormDto? ImperativeFs { get; set; }
    public WordFormDto? ImperativeMp { get; set; }

    public Dictionary<string, string> ExtraInfo { get; set; } = [];

    public List<VerbModel> VerbModels { get; set; } = [];
    public List<Gizra> Gizras { get; set; } = [];
    public List<VerbTag> Tags { get; set; } = [];

    public VerbDto()
    {
    }
}


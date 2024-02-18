using HebrewVerb.SharedKernel.Abstractions;

namespace HebrewVerb.Domain.Entities;

public class WordForm : BaseEntity<int>
{
    public static readonly WordForm Default = new();

    public string Hebrew { get; set; } = String.Empty;

    public string HebrewNiqqud { get; set; } = String.Empty;

    public string? TranscriptionRus { get; set; }

    public int StressLetterRus { get; set; }

    public string? TranscriptionEng {  get; set; }

    public int StressLetterEng { get; set; }

    private WordForm() { }

    public WordForm(string hebrew,
        string hebrewNiqqud,
        string? transcriptionRus = null,
        int stressLetterRus = 0,
        string? transcriptionEng = null,
        int stressLetterEng = 0)
    {
        Hebrew = hebrew;
        HebrewNiqqud = hebrewNiqqud;
        TranscriptionRus = transcriptionRus;
        StressLetterRus = stressLetterRus;
        TranscriptionEng = transcriptionEng;
        StressLetterEng = stressLetterEng;
    }

    public void AddTranscriptionRus(string transcriptionRus, int stressRus = 0)
    {
        TranscriptionRus = transcriptionRus;
        StressLetterRus = stressRus;
    }

    public void AddTranscriptionEng(string transcriptionEng, int stressEng = 0)
    {
        TranscriptionEng = transcriptionEng;
        StressLetterEng = stressEng;
    }
}

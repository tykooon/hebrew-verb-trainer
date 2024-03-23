using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Models;

public class PrepositionDto
{
    public int Id { get; set; }
    public WordFormDto BaseForm { get; set; } = WordFormDto.Default;
    public string TranslationRus { get; set; } = "";
    public string TranslationEng { get; set; } = "";
    public int LangId { get; set; } = (int)Language.Russian;

    public WordFormDto MS1 { get; set; } = WordFormDto.Default;
    public WordFormDto MP1 { get; set; } = WordFormDto.Default;
    public WordFormDto MS2 { get; set; } = WordFormDto.Default;
    public WordFormDto MP2 { get; set; } = WordFormDto.Default;
    public WordFormDto FS2 { get; set; } = WordFormDto.Default;
    public WordFormDto FP2 { get; set; } = WordFormDto.Default;
    public WordFormDto MS3 { get; set; } = WordFormDto.Default;
    public WordFormDto MP3 { get; set; } = WordFormDto.Default;
    public WordFormDto FS3 { get; set; } = WordFormDto.Default;
    public WordFormDto FP3 { get; set; } = WordFormDto.Default;

    public PrepositionDto()
    { }
}

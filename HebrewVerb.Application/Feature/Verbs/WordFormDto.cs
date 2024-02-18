namespace HebrewVerb.Application.Feature.Verbs;

public record WordFormDto(string Hebrew, string HebrewNikkud, string Transcript, int Stress)
{
    public static readonly WordFormDto Default = new("", "", "", -1);
}

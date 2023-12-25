namespace HebrewVerb.Application.Models;

public class Conjugation
{
    public VerbForm? Infinitive { get; set; }
    public NumberPair? Present { get; set; }
    public PersonSet? Past { get; set; }
    public PersonSet? Future { get; set; }
    public NumberPair? Imperative { get; set; }
}

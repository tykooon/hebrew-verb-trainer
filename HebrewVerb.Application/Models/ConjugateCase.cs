namespace HebrewVerb.Application.Models;

public enum ConjugateCase
{
    Infinitive  = 0b0000_0000_0000_0000,

    Male        = 0b0000_0001_0000_0000,
    Female      = 0b0000_0001_0000_0001,

    Singular    = 0b0000_0010_0000_0000,
    Plural      = 0b0000_0010_0000_0010,


    Past        = 0b0000_0100_0000_0000,
    Future      = 0b0000_0100_0000_0100,
    Present     = 0b0000_0100_0000_1000,
    Imperative  = 0b0000_0100_0000_1100,

    NoPerson    = 0b0000_1000_0000_0000,
    First       = 0b0000_1000_0001_0000,
    Second      = 0b0000_1000_0010_0000,
    Third       = 0b0000_1000_0011_0000
}

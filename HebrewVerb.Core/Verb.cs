namespace HebrewVerb.Core;

public class Verb : Entity<int>
{
    public string? Inf {  get; set; }
    public string? InfT { get; set; }
    public Binyan Binyan { get; set; }
    public Shoresh? Shoresh { get; set; }

    public string? PreMS { get; set; }
    public string? PreFS { get; set; }
    public string? PreMP { get; set; }
    public string? PreFP { get; set; }

    public string? Pas1S { get; set; }
    public string? Pas1P { get; set; }
    public string? Pas2MS { get; set; }
    public string? Pas2FS { get; set; }
    public string? Pas2MP { get; set; }
    public string? Pas2FP { get; set; }
    public string? Pas3MS { get; set; }
    public string? Pas3FS { get; set; }
    public string? Pas3P { get; set; }

    public string? Fut1S { get; set; }
    public string? Fut1P { get; set; }
    public string? Fut2MS { get; set; }
    public string? Fut2FS { get; set; }
    public string? Fut2P { get; set; }
    public string? Fut3MS { get; set; }
    public string? Fut3FS { get; set; }
    public string? Fut3P { get; set; }

    public string? Imp2MS { get; set; }
    public string? Imp2FS { get; set; }
    public string? Imp2P { get; set; }

    public string? PreMST { get; set; }
    public string? PreFST { get; set; }
    public string? PreMPT { get; set; }
    public string? PreFPT { get; set; }

    public string? Pas1ST { get; set; }
    public string? Pas1PT { get; set; }
    public string? Pas2MST { get; set; }
    public string? Pas2FST { get; set; }
    public string? Pas2MPT { get; set; }
    public string? Pas2FPT { get; set; }
    public string? Pas3MST { get; set; }
    public string? Pas3FST { get; set; }
    public string? Pas3PT { get; set; }

    public string? Fut1ST { get; set; }
    public string? Fut1PT { get; set; }
    public string? Fut2MST { get; set; }
    public string? Fut2FST { get; set; }
    public string? Fut2PT { get; set; }
    public string? Fut3MST { get; set; }
    public string? Fut3FST { get; set; }
    public string? Fut3PT { get; set; }

    public string? Imp2MST { get; set; }
    public string? Imp2FST { get; set; }
    public string? Imp2PT { get; set; }

    public string? Translate { get; set; }
    public bool IsArchaic {  get; set; } = false;
    public bool IsLiterary { get; set;} = false;
    public bool IsRare { get; set;} = false;
}

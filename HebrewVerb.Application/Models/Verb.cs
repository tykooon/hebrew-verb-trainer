namespace HebrewVerb.Application.Models;

public class Verb
{
    public int Id { get; set; }
    public Conjugation? Conjugation { get; set; }
    public string? Translate { get; set; }

    public VerbForm? this[int zman, int guf, int camot, int min]
    {
        get
        {
            NumberPair? num;
            if (zman == 1)
            {
                PersonSet? personSet = zman switch
                {
                    1 => Conjugation?.Past,
                    2 => Conjugation?.Future,
                    _ => null
                };
                num = guf switch
                {
                    1 => personSet?.First,
                    2 => personSet?.Second,
                    3 => personSet?.Third,
                    _ => null
                };
            }
            else
            {
                num = zman switch
                {
                    1 => Conjugation?.Present,
                    2 => Conjugation?.Imperative,
                    _ => null
                };
            }
            var res = camot switch
            {
                1 => num?.Singular,
                2 => num?.Plural,
                _ => null
            };

            var final = min switch
            {
                1 => res?.Male,
                2 => res?.Female,
                _ => null

            };
            return final;
        }
    }

    public Verb(Core.Verb verb)
    {
        Conjugation = new()
        {
            Infinitive = new(verb.Inf, verb.InfT),
            Present = new(
                new(
                    new(verb.PreMS, verb.PreMST),
                    new(verb.PreFS, verb.PreFST)),
                new(
                    new(verb.PreMP, verb.PreMPT),
                    new(verb.PreFP, verb.PreFPT))),
            Past = new(
                new(
                    new(
                        new(verb.Pas1S, verb.Pas1ST),
                        new(verb.Pas1S, verb.Pas1ST)),
                    new(
                        new(verb.Pas1P, verb.Pas1PT),
                        new(verb.Pas1P, verb.Pas1PT))),
                new(
                    new(
                        new(verb.Pas2MS, verb.Pas2MST),
                        new(verb.Pas2FS, verb.Pas2FST)),
                    new(
                        new(verb.Pas2MP, verb.Pas2MPT),
                        new(verb.Pas2FP, verb.Pas2FPT))),
                                new(
                    new(
                        new(verb.Pas3MS, verb.Pas3MST),
                        new(verb.Pas3FS, verb.Pas3FST)),
                    new(
                        new(verb.Pas3P, verb.Pas3PT),
                        new(verb.Pas3P, verb.Pas3PT)))),
            Future = new(
                new(
                    new(
                        new(verb.Fut1S, verb.Fut1ST),
                        new(verb.Fut1S, verb.Fut1ST)),
                    new(
                        new(verb.Fut1P, verb.Fut1PT),
                        new(verb.Fut1P, verb.Fut1PT))),
                new(
                    new(
                        new(verb.Fut2MS, verb.Fut2MST),
                        new(verb.Fut2FS, verb.Fut2FST)),
                    new(
                        new(verb.Fut2P, verb.Fut2PT),
                        new(verb.Fut2P, verb.Fut2PT))),
                                new(
                    new(
                        new(verb.Fut3MS, verb.Fut3MST),
                        new(verb.Fut3FS, verb.Fut3FST)),
                    new(
                        new(verb.Fut3P, verb.Fut3PT),
                        new(verb.Fut3P, verb.Fut3PT)))),
            Imperative = new(
                new(
                    new(verb.Imp2MS, verb.Imp2MST),
                    new(verb.Imp2FS, verb.Imp2FST)),
                new(
                    new(verb.Imp2P, verb.Imp2PT),
                    new(verb.Imp2P, verb.Imp2PT)))
        };
    }

    public VerbForm? GetForm(int id)
    {
        return Conjugation?.Past?.Second?.Singular?.Male;
    }
}



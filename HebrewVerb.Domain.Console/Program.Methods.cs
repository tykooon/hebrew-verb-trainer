using static System.Console;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;

internal partial class Program
{
    public static void ShoreshComponents()
    {
        var shor = new Shoresh("קרבן");

        WriteLine(shor.First);
        WriteLine(shor.Second);
        WriteLine(shor.Third);

        WriteLine(shor.WithDots);
    }

    static void RandomGufGeneration()
    {
        var dict = new Dictionary<string, int>();
        string guf;

        for (int i = 0; i < 10000; i++)
        {
            guf = Randomized.GufFor(Zman.Infinitive).Name;
            _ = dict.ContainsKey(guf) ? dict[guf]++ : dict[guf] = 1;
        }

        foreach (var key in dict.Keys.OrderBy(x => dict[x]))
        {
            Console.WriteLine("{0} : {1}", key, dict[key]);
        }
    }
}

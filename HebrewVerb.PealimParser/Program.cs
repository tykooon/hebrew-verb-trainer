using HebrewVerb.PealimParser;
using System.Runtime.InteropServices;
using System.Text.Json;

const string VerbUrl = "https://www.pealim.com/ru/dict/1296-lesovev/";

//HebrewConsoleOn();
var result = VerbParser.FromUri(VerbUrl);

if (!result.IsSuccess)
{
    Console.WriteLine(string.Join(", ", result.Errors));
    return;
}

var json = JsonSerializer.Serialize(result.Value);
File.WriteAllText("out.txt", json);


static void HebrewConsoleOn()
{
    SetConsoleOutputCP(65001);
    SetConsoleCP(65001);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleOutputCP(uint wCodePageID);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleCP(uint wCodePageID);
}


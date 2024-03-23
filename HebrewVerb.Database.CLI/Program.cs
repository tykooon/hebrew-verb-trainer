using Newtonsoft.Json;
using System.Text;


Console.WriteLine("CLI tool for filling Hebrew Verb Database with data from txt-file via localhost api.");
if (args.Length == 0)
{
    Console.WriteLine("Error: No arguments were provided. Use option --help or -h for list of commands.");
    return;
}

switch (args[0])
{
    case "add":
        await AddFromFile(args[1..]);
        break;
    case "--help" or "-h":
        Console.WriteLine("""
                          List of commant for this CLI:
                             add <file.txt>   : adds verbs by the pealim website links from the txt-file to database. String may start with '*' to load both active and passive forms of verb.
                          """);
        break;
    default:
        Console.WriteLine($"Unknown command {args[0]}. Use option --help or -h for list of commands." );
        break;
}

return;



static async Task AddFromFile(string[] args)
{
    if (args.Length == 0)
    {
        Console.WriteLine("Missing file name as argument for command add...");
        return;
    }

    var path = Directory.GetCurrentDirectory();
    path = Path.GetFullPath(path);
    var filename = Path.Combine(path, args[0]);
    if (!File.Exists(filename))
    {
        Console.WriteLine($"File {args[0]} was not found in current directory. Unable to execute command add...");
        return;
    }

    string[] source = [];
    try
    {
        source = File.ReadAllLines(filename);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unable to read file {args[0]}");
        Console.WriteLine("Error info:" + ex.Message);
        return;
    }

    using var connection = new HttpClient();
    connection.BaseAddress = new Uri("https://localhost:7048/api/Verb/addFromUri");
    foreach (string line in source)
    {
        if (string.IsNullOrEmpty(line))
        {
            continue;
        }


        var verbId = line[..^1].Split('/').Last();

        if (line[0] == '*')
        {
            var res = await SendPost(connection, line[1..], false);
            Console.WriteLine(verbId + ": \t" + res);
            res = await SendPost(connection, line[1..], true);
            Console.WriteLine(verbId + ": \t" + res);
        }
        else
        {
            var res = await SendPost(connection, line, false);
            Console.WriteLine(verbId + ": \t" + res);
        }
    }
}

static async Task<string> SendPost(HttpClient client, string verbUri, bool isPassive)
{
    try
    {
        var content = JsonConvert.SerializeObject(new VerbPostBody(verbUri, isPassive));
        var data = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7048/api/Verb/addFromUri", data);
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
    catch (Exception ex)
    {
        return $"ERROR. {ex.Message}";
    }

}

record VerbPostBody(string Url, bool IsPassive = false);
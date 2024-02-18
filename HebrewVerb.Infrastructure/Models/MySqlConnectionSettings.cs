namespace HebrewVerb.WebApp.Models;

public class MySqlConnectionSettings
{
    public string Server { get; set; } = "";
    public int Port {  get; set; }
    public string Uid { get; set; } = "";
    public string Password { get; set; } = "";

    public string? UpdateConnectionString(string? str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            str = str
                .Replace("{SERVER}", Server)
                .Replace("{PORT}", Port.ToString())
                .Replace("{USER}", Uid)
                .Replace("{PASSWORD}", Password);
        }
        
        return str;
    }
}

using System.Text.Json;

namespace HebrewVerb.Application.Services;

public static class ExtraInfoService
{
    public static JsonSerializerOptions options = new JsonSerializerOptions();

    public static string ToJson(this Dictionary<string, string> dict)
    {
        return JsonSerializer.Serialize(dict, options);
    }

    public static Dictionary<string, string> ToExtraInfo(this string json)
    {
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? [];
    }

    public static bool EqualsDictionary(Dictionary<string, string> d1, Dictionary<string, string> d2)
    {
        if (d1 == null && d2 == null)
        {
            return true;
        }

        if (d1 == null || d2 == null)
        {
            return false;
        }

        if(d1.Count != d2.Count)
        {
            return false;
        }

        foreach(var key in d1.Keys)
        {
            if (!d2.TryGetValue(key, out string? value))
            { 
                return false;
            };

            if (d1[key] != value)
            {
                return false;
            }
        }

        return true;
    }

    public static int ExtraInfoHashCode(Dictionary<string, string> dict)
    {
        int hash = 0;
        foreach (var key in dict.Keys)
        {
            hash += key.GetHashCode() + dict[key].GetHashCode();
        }
        return hash;
    }
}


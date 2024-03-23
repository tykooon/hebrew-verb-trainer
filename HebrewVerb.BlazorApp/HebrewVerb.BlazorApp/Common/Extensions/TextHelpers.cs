using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.BlazorApp.Common.Extensions;

public static class TextHelpers
{
    public static string InfinitiveFormatted(this VerbInfo verb) => 
        verb.IsPassive() ? $"({verb.Infinitive})" : verb.Infinitive;

    public static bool IsPassive(this VerbInfo verb) => 
        verb.Binyan.IsPassive;
}

using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Common.Extensions;

public static class FilterExtensions
{
    public static bool EqualsTo(Filter? f1, Filter? f2)
    {
        return (f1 != null) 
            && (f2 != null)
            && f1.Binyans.SetEquals(f2.Binyans)
            && f1.Gizras.SetEquals(f2.Gizras)
            && f1.VerbModels.SetEquals(f2.VerbModels)
            && f1.Zmans.SetEquals(f2.Zmans);
    }

    public static int GetHashCode(this Filter? filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return filter.Binyans.Select(b => b.GetHashCode()).Sum() +
            filter.Gizras.Select(b => b.GetHashCode()).Sum() +
            filter.VerbModels.Select(b => b.GetHashCode()).Sum() +
            filter.Zmans.Select(b => b.GetHashCode()).Sum();
    }

}

namespace HebrewVerb.BlazorApp.Common;

public static class Constants
{
    public readonly static Dictionary<string, MudBlazor.Color> ZmanColor = new()
    {
        ["הווה"] = MudBlazor.Color.Success,
        ["עבר"] = MudBlazor.Color.Warning,
        ["עתיד"] = MudBlazor.Color.Info,
        ["ציווי"] = MudBlazor.Color.Error,
        [""] = MudBlazor.Color.Default,
    };

    // Constants for cache key names
    public const string GizraList = nameof(GizraList);
    public const string VerbModelList = nameof(VerbModelList);

}

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

    public const string BinyanList = nameof(BinyanList);
    public const string ZmanList = nameof(ZmanList);
    public const string GizraList = nameof(GizraList);
    public const string VerbModelList = nameof(VerbModelList);

}

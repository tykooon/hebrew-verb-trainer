using System.ComponentModel;

namespace HebrewVerb.Core;

public enum Binyan
{
    Undefined = 0,
    [Description("ПААЛЬ")]
    Paal = 1,
    [Description("ПИЭЛЬ")]
    Piel = 2,
    [Description("hИФЪИЛЬ")]
    Hifil = 4,
    [Description("НИФЪАЛЬ")]
    Nifal = 8,
    [Description("hИТПАЭЛЬ")]
    Hitpael = 16,
    [Description("ПУАЛЬ")]
    Pual = 32,
    [Description("hУФЪАЛЬ")]
    Hufal = 64,
}

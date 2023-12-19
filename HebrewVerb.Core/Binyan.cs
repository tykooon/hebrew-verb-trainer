using System.ComponentModel;

namespace HebrewVerb.Core;

public enum Binyan
{
    Undefined = 0,
    [Description("ПААЛЬ")]
    Paal = 1,
    [Description("ПИЭЛЬ")]
    Piel,
    [Description("hИФЪИЛЬ")]
    Hifil,
    [Description("НИФЪАЛЬ")]
    Nifal,
    [Description("ПУАЛЬ")]
    Pual,
    [Description("hУФЪАЛЬ")]
    Hufal,
    [Description("hИТПАЭЛЬ")]
    Hitpael
}

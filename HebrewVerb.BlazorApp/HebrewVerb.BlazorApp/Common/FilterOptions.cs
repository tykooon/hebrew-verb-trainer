using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.BlazorApp.Common;

public class FilterOptions
{
    public static readonly HashSet<Binyan> BinyanOptions = [
        Binyan.Paal,
        Binyan.Piel,
        Binyan.Hifil,
        Binyan.Hitpael,
        Binyan.Nifal,
        Binyan.Pual,
        Binyan.Hufal];

    public static readonly HashSet<Zman> ZmanOptions = [
        Zman.Past,
        Zman.Present,
        Zman.Future,
        Zman.Imperative];

    public static readonly HashSet<Gizra> GizraOptions = [
        Gizra.Shlemim,
        Gizra.Merubaim,
        Gizra.Khaphan,
        Gizra.Khaphits,
        Gizra.Napha,
        Gizra.Naphiu,
        Gizra.Nayui,
        Gizra.Nala,
        Gizra.Naliah,
        Gizra.Kfulim];

    public static readonly HashSet<VerbModel> VerbModelOptions = [
        VerbModel.Efol,
        VerbModel.Efal,
        VerbModel.YotseDophen,
        VerbModel.PeAlef,
        VerbModel.PeHeyAyn,
        VerbModel.PeChet,
        VerbModel.PeZayn,
        VerbModel.PeSamechShin,
        VerbModel.PeResh,
        VerbModel.PeTetTaw,
        VerbModel.AynAlef,
        VerbModel.AynHeyChetAyn,
        VerbModel.AynResh,
        VerbModel.LamedAlef,
        VerbModel.LamedChetAyn,
        VerbModel.LamedTav];

    public static readonly HashSet<VerbTag> VerbTagOptions = [
        VerbTag.Archaic,
        VerbTag.Literary,
        VerbTag.Slang,
        VerbTag.Formal,
        VerbTag.Obsolete,
        VerbTag.Rare,
        VerbTag.Unformal,
        VerbTag.Tag128];

}

using HebrewVerb.SharedKernel.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace HebrewVerb.SharedKernel.Enums;

[JsonConverter(typeof(VerbModelJsonConverter))]
public class  VerbModel : HebrewTagFlag<VerbModel>, IHebrewTagFlag
{
    private VerbModel(string name, int value, string nameHebrew, string nameRussian, params Binyan[] binyans) :
        base(name, value, nameHebrew, nameRussian, name)
    {
        _binyans = new Binyan[binyans.Length];
        binyans.CopyTo(_binyans, 0);
    }

    private readonly Binyan[] _binyans = [];

    public IEnumerable<Binyan> Binyans => _binyans;

    public bool HasBinyan(Binyan binyan) => _binyans.Contains(binyan);

    #region Static members

    public static readonly VerbModel Undefined = new(nameof(Undefined), 0, "אין גזרה", "не определена");

    public static readonly VerbModel Efol = new(nameof(Efol), 1,
        "אפעול",
        "Эфоль. В будущем времени в первом лице есть 'ו'",
        Binyan.Paal);
    public static readonly VerbModel Efal = new(nameof(Efal), 2,
        "אפעל",
        "Эфаль. В будущем времени в первом лице отсутствует 'ו'",
        Binyan.Paal);
    public static readonly VerbModel PeAlef = new(nameof(PeAlef), 3,
        "פ\'\'א",
        "Первая корневая - гортанная א (звучащая).",
        Binyan.Paal, Binyan.Nifal, Binyan.Hifil);
    public static readonly VerbModel PeHeyAyn = new(nameof(PeHeyAyn), 4,
        "פ\'\'ה'/ע'",
        "Первая корневая - гортанная ה'/ע'.",
        Binyan.Paal, Binyan.Nifal, Binyan.Hifil, Binyan.Hufal);
    public static readonly VerbModel PeChet = new(nameof(PeChet), 5,
        "פ\'\'ח'",
        "Первая корневая - гортанная ח'.",
        Binyan.Paal, Binyan.Nifal, Binyan.Hifil);
    public static readonly VerbModel PeResh = new(nameof(PeResh), 6,
        "פ\'\'ר'",
        "Первая корневая - гортанная ר'.",
        Binyan.Nifal);
    public static readonly VerbModel AynHeyChetAyn = new(nameof(AynHeyChetAyn), 7,
        "ע\'\'ה'/ח'/ע'",
        "Вторая корневая - гортанная ה'/ח'/ע'.",
        Binyan.Paal, Binyan.Nifal, Binyan.Pual);
    public static readonly VerbModel AynAlef = new(nameof(AynAlef), 8,
        "ע\'\'א",
        "Вторая корневая - гортанная ע'.",
        Binyan.Paal, Binyan.Piel);
    public static readonly VerbModel AynResh = new(nameof(AynResh), 9,
        "ע\'\'ר",
        "Вторая корневая - гортанная ר'.",
        Binyan.Piel, Binyan.Pual);
    public static readonly VerbModel LamedAlef = new(nameof(LamedAlef), 10,
        "ל\'\'א",
        "Последняя корневая - гортанная א'.",
        Binyan.List.Where(b => b.Id != 0).ToArray());
    public static readonly VerbModel LamedChetAyn = new(nameof(LamedChetAyn), 11,
        "ל\'\'ח'/ע'",
        "Последняя корневая - гортанная ח'/ע'.",
        Binyan.List.Where(b => b.Id != 0).ToArray());
    public static readonly VerbModel LamedTav = new(nameof(LamedTav), 12,
        "ל\'\'ת'",
        "Последняя корневая - ת'.",
        Binyan.Paal, Binyan.Nifal);
    public static readonly VerbModel YotseDophen = new(nameof(YotseDophen), 13,
        "יוצא דופן",
        "Глаголы-исключения",
        Binyan.Paal, Binyan.Nifal);
    public static readonly VerbModel PeSamechShin = new(nameof(PeSamechShin), 14,
        "פ\'\'ס'/ש'",
        "Первая корневая - ס'/ש'.",
        Binyan.Hitpael);
    public static readonly VerbModel PeTsadi = new(nameof(PeTsadi), 15,
        "פ\'\'צ'",
        "Первая корневая - צ'.",
        Binyan.Hitpael);
    public static readonly VerbModel PeZayn = new(nameof(PeZayn), 16,
        "פ\'\'ס'/ש'",
        "Первая корневая - ס'/ש'.",
        Binyan.Hitpael);
    public static readonly VerbModel PeTetTaw = new(nameof(PeTetTaw), 17,
        "פ\'\'ט'/ת'",
        "Первая корневая - ט'/ת'.",
        Binyan.Hitpael);

    #endregion
}

public class VerbModelJsonConverter : JsonConverter<VerbModel>
{
    public override VerbModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        VerbModel.FromName(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, VerbModel value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}

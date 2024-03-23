using HebrewVerb.SharedKernel.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HebrewVerb.SharedKernel.Enums;

[JsonConverter(typeof(GizraJsonConverter))]
public class Gizra : HebrewTagFlag<Gizra>, IHebrewTagFlag
{
    private Gizra(string name, int value, string nameHebrew, string nameRussian, params Binyan[] binyans) :
        base(name, value, nameHebrew, nameRussian, name)
    {
        _binyans = new Binyan[binyans.Length];
        binyans.CopyTo(_binyans, 0);
    }

    private readonly Binyan[] _binyans = [];

    public IEnumerable<Binyan> Binyans => _binyans;

    public bool HasBinyan (Binyan binyan) => _binyans.Contains(binyan);

    #region Static members

    public static readonly Gizra Undefined = new(nameof(Undefined), 0, "אין גזרה", "не определена");

    // רגלים
    public static readonly Gizra Shlemim = new(nameof(Shlemim), 1,
        "שלמים",
        "Корень из 3 букв. Все корневые сохраняются при спряжении.",
        Binyan.List.Where(b => b.Id != 0).ToArray());
    public static readonly Gizra Merubaim = new(nameof(Merubaim), 2,
        "מרובעים",
        "Корень из 4 букв. Все корневые сохраняются при спряжении.",
        Binyan.Piel, Binyan.Pual, Binyan.Hitpael);

    // חסרים
    public static readonly Gizra Khaphan = new(nameof(Khaphan), 3,
        "חפ\'\'נ",
        "Первая корневая - исчезающая נ.",
        Binyan.Paal, Binyan.Nifal, Binyan.Hifil, Binyan.Hufal);
    public static readonly Gizra Khaphits = new(nameof(Khaphits), 4,
        "חפי\'\'צ",
        "Первая корневая - исчезающая י перед צ",
        Binyan.Paal, Binyan.Nifal, Binyan.Hifil, Binyan.Hufal);

    // נחים
    public static readonly Gizra Naliah = new(nameof(Naliah), 5,
        "נלי\'\'ה",
        "Последняя корневая - слабая י/ה.",
        Binyan.List.Where(b => b.Id != 0).ToArray());
    public static readonly Gizra Nala = new(nameof(Nala), 6,
        "נל\'\'א",
        "Последняя корневая - слабая א.",
        Binyan.List.Where(b => b.Id != 0).ToArray());
    public static readonly Gizra Napha = new(nameof(Napha), 7,
        "נפ\'\'א",
        "Первая корневая - слабая א.",
        Binyan.Paal);
    public static readonly Gizra Naphiu = new(nameof(Naphiu), 8,
        "נפי\'\'ו",
        "Первая корневая - слабая י/ו.",
        Binyan.Paal, Binyan.Hifil, Binyan.Hufal);
    public static readonly Gizra Nayui = new(nameof(Nayui), 9,
        "נעו\'\'י",
        "Вторая корневая - слабая ו/י.",
        Binyan.List.Where(b => b.Id != 0).ToArray());

    // כפולים
    public static readonly Gizra Kfulim = new(nameof(Kfulim), 10,
        "כפולים (ע\'\'ע)",
        "Две последние буквы корня совпадают.",
        Binyan.Paal, Binyan.Piel, Binyan.Hifil, Binyan.Hufal, Binyan.Hitpael);

    #endregion
}

public class GizraJsonConverter : JsonConverter<Gizra>
{
    public override Gizra? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Gizra.FromName(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, Gizra value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}


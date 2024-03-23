using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace HebrewVerb.SharedKernel.TagFlagEnum;

public abstract class TagFlagEnum<TEnum>
    where TEnum : TagFlagEnum<TEnum>
{
    private readonly string _name = string.Empty;
    private readonly int _flag;
    private readonly int _id;

    public string Name => _name;
    public int Flag => _flag;
    public int Id => _id;

    static readonly Lazy<Dictionary<string, TEnum>> _fromName =
        new(() => GetAllOptions().ToDictionary(item => item.Name));

    static readonly Lazy<Dictionary<int, TEnum>> _fromId =
        new (() => GetAllOptions().ToDictionary(item => item.Id));

    public static IReadOnlyCollection<TEnum> List => _fromName.Value.Values.ToList().AsReadOnly();


    protected TagFlagEnum(string name, int id)
    {
        ArgumentNullException.ThrowIfNull(nameof(name));

        _name = name;
        _id = id;
        _flag = PowerOfTwo(id);
    }

    public static TEnum FromName(string name)
    {
        ArgumentNullException.ThrowIfNull(nameof(name));
        
        if (!_fromName.Value.TryGetValue(name, out var result))
        {
                throw new KeyNotFoundException($"Tag with name {name} not found in collection of {typeof(TEnum).Name}");
        }

        return result;
    }

    public static bool TryFromName(string name, [MaybeNullWhen(false)] out TEnum result)
    {
        ArgumentNullException.ThrowIfNull(nameof(name));

        return _fromName.Value.TryGetValue(name, out result);
    }

    public static TEnum FromId(int id)
    {
        GuardExponentRange(id);

        if (!_fromId.Value.TryGetValue(id, out var result))
        {
            throw new KeyNotFoundException($"Tag with id {id} not found in collection of {typeof(TEnum).Name}");
        }

        return result;
    }

    public static bool TryFromId(int id, out TEnum? result)
    {
        GuardExponentRange(id);

        return _fromId.Value.TryGetValue(id, out result);
    }

    public bool IsIncluded(int value) =>
        (value & _flag) != 0;

    public static IEnumerable<TEnum> GetTagsFromFlag(int value) =>
        GetAllOptions().Where(f => f.IsIncluded(value));

    public static IEnumerable<TEnum> GetTagsFromIds(IEnumerable<int> ids) =>
        GetAllOptions().Where(f => ids.Contains(f.Id));

    public static string GetTagsAsString(int value)
    {
        var tagFlags = GetTagsFromFlag(value);
        return FormatEnumListString(tagFlags);
    }

    public override string ToString() =>
        _name;

    public override int GetHashCode() =>
        _flag.GetHashCode();

    public override bool Equals(object? obj) =>
        (obj is TagFlagEnum<TEnum> other) && Equals(other);

    public virtual bool Equals(TagFlagEnum<TEnum> other)
    {
        if (Object.ReferenceEquals(this, other))
            return true;

        if (other is null)
            return false;

        return _flag.Equals(other._flag);
    }

    protected static int PowerOfTwo(int exponent)
    {
        GuardExponentRange(exponent);
        return PowerOfTwoRecursive(1, exponent);

        static int PowerOfTwoRecursive(int value, int exponent)
        {
            if(exponent == 0)
            {
                return value;
            }
            return PowerOfTwoRecursive(value << 1, --exponent);
        }
    }

    protected static void GuardExponentRange(int exponent)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(exponent);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(exponent, 32);
    }

    private static IEnumerable<TEnum> GetAllOptions()
    {
        Type baseType = typeof(TEnum);
        IEnumerable<Type> enumTypes = Assembly.GetAssembly(baseType)?
            .GetTypes().Where(t => baseType.IsAssignableFrom(t)) ?? [];

        List<TEnum> options = [];
        foreach (Type enumType in enumTypes)
        {
            List<TEnum> typeEnumOptions = enumType.GetFieldsOfType<TEnum>();
            options.AddRange(typeEnumOptions);
        }

        return options.OrderBy(t => t.Id);
    }

    private static string FormatEnumListString(IEnumerable<TEnum> enumInputList)
    {
        var enumList = enumInputList.ToList();
        var sb = new StringBuilder();

        foreach (var flag in enumList)
        {
            sb.Append(flag.Name);
            if (enumList.Last().Name != flag.Name && enumList.Count > 1)
            {
                sb.Append(", ");
            }
        }

        return sb.ToString();
    }
}

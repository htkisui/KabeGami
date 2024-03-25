using System.Reflection;

namespace KabeGami.Domain.Common.Primitives;
public abstract class Enumeration<TEnum> 
    : IEquatable<Enumeration<TEnum>>
        where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> _enumerations = CreateEnumerations();

    public string Name { get; }
    public int Value { get; }

    protected Enumeration(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public static TEnum? FromValue(int value)
    {
        return _enumerations.TryGetValue(
            value,
            out TEnum? enumeration) 
                ? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return _enumerations
            .Values
            .SingleOrDefault(e => e.Name == name);
    }

    public static List<string> GetNames()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.Select(x => x.Name).ToList();
    }

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null || GetType() != other.GetType()) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => obj is Enumeration<TEnum> other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();
}

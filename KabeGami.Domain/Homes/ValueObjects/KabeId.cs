using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Homes.ValueObjects;
public sealed class KabeId : ValueObject
{
    public Guid Value { get; private set; }

    private KabeId(Guid value)
    {
        Value = value;
    }

    private KabeId() { }

    public static KabeId Create(Guid value) => new(value);
    public static KabeId CreateEmpty() => new(Guid.Empty);
    public static KabeId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

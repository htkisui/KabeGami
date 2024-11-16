using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Homes.ValueObjects;
public sealed class HomeId : ValueObject
{
    public Guid Value { get; private set; }

    private HomeId(Guid value)
    {
        Value = value;
    }

    private HomeId() { }

    public static HomeId Create(Guid value) => new(value);
    public static HomeId CreateEmpty() => new(Guid.Empty);
    public static HomeId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

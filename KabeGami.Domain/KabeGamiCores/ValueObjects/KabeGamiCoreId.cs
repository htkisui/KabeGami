using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.KabeGamis.ValueObjects;
public sealed class KabeGamiCoreId : ValueObject
{
    public Guid Value { get; private set; }

    private KabeGamiCoreId(Guid value)
    {
        Value = value;
    }

    private KabeGamiCoreId() { }

    public static KabeGamiCoreId Create(Guid value) => new(value);
    public static KabeGamiCoreId CreateEmpty() => new(Guid.Empty);
    public static KabeGamiCoreId CreateUnique() => new(Guid.NewGuid());
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

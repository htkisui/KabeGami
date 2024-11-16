using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Homes.ValueObjects;

namespace KabeGami.Domain.Images.ValueObjects;
public sealed class ImageId : ValueObject
{
    public Guid Value { get; private set; }

    private ImageId(Guid value)
    {
        Value = value;
    }

    private ImageId() { }

    public static ImageId Create(Guid value) => new(value);
    public static ImageId CreateEmpty() => new(Guid.Empty);
    public static ImageId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

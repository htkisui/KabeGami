using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Galleries.ValueObjects;
public sealed class SubGalleryId : ValueObject
{
    public Guid Value { get; }

    private SubGalleryId(Guid value)
    {
        Value = value;
    }

    public static SubGalleryId Create(Guid value) => new(value);
    public static SubGalleryId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

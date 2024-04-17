using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Galleries.ValueObjects;
public sealed class GalleryId : ValueObject
{
    public Guid Value { get; private set; }

    private GalleryId(Guid value)
    {
        Value = value;
    }

    private GalleryId() { }

    public static GalleryId Create(Guid value) => new(value);
    public static GalleryId CreateEmpty() => new(Guid.Empty);
    public static GalleryId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

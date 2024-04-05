using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;

namespace KabeGami.Domain.Galleries;
public sealed class Gallery : AggregateRoot<GalleryId>
{
    private readonly List<SubGalleryId> _subGalleryIds = [];

    public string Name { get; }
    public IReadOnlyList<SubGalleryId> SubGalleryIds => _subGalleryIds.AsReadOnly();
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private Gallery(
        GalleryId galleryId,
        string name,
        DateTime createdDateTime,
        DateTime updatedDateTime) 
        : base(galleryId)
    {
        Name = name;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Gallery Create(
        string name,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new(
            GalleryId.CreateUnique(),
            name,
            createdDateTime,
            updatedDateTime);
    }
}

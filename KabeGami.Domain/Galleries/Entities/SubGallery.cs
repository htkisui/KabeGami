using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Domain.Galleries.Entities;
public sealed class SubGallery : Entity<SubGalleryId>
{
    public string Name { get; }
    public List<ImageId> ImageIds { get; } = [];

    private SubGallery(
        SubGalleryId subGalleryId,
        string name) 
        : base(subGalleryId)
    {
        Name = name;
    }

    public static SubGallery Create(string name)
    {
        return new(
            SubGalleryId.CreateUnique(),
            name);
    }
}

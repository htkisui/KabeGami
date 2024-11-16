using ErrorOr;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Images.ValueObjects;
using MediatR;

namespace KabeGami.Domain.Galleries;
public sealed class Gallery : AggregateRoot<GalleryId>
{
    public string Name { get; private set; }
    public IReadOnlyList<ImageId> ImageIds => _imageIds.AsReadOnly();
    private readonly List<ImageId> _imageIds = [];
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

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

#pragma warning disable CS8618
    private Gallery() { }
#pragma warning restore CS8618

    public static ErrorOr<Gallery> Create(
        string name)
    {
        return new Gallery(
            GalleryId.CreateUnique(),
            name,
            DateTime.Now,
            DateTime.Now);
    }

    public ErrorOr<Unit> SetImageIds(List<ImageId> imageIds)
    {
        _imageIds.Clear();
        _imageIds.AddRange(imageIds);

        return Unit.Value;
    }
}

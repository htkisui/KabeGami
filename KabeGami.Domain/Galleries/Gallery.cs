using ErrorOr;
using KabeGami.Domain.Common.Errors;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.Entities;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Images.ValueObjects;
using MediatR;

namespace KabeGami.Domain.Galleries;
public sealed class Gallery : AggregateRoot<GalleryId>
{
    public string Name { get; private set; }
    public SubGalleryId SubGalleryId { get; private set; } = SubGalleryId.CreateEmpty();
    public IReadOnlyList<SubGallery> SubGalleries => _subGalleries.AsReadOnly();
    private readonly List<SubGallery> _subGalleries = [];
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

    public ErrorOr<Unit> AddImageIdsToSubGallery(
        SubGalleryId subGalleryId,
        List<ImageId> imageIds)
    {
        var subGallery = _subGalleries.SingleOrDefault(sg => sg.Id == subGalleryId);
        if (subGallery is null)
        {
            return Errors.Gallery.SubGalleryNotFound(subGalleryId);
        }

        subGallery.SetImageIds(imageIds);

        return Unit.Value;
    }

    public ErrorOr<Unit> CreateSubGallery(
        string name,
        string shortcutKey,
        string cronSchedule)
    {
        if (_subGalleries.Any(sg => sg.Name == name))
        {
            return Errors.Gallery.SubGalleryNameDuplicated(name);
        }
        
        if(_subGalleries.Any(sg => sg.Combination == shortcutKey))
        {
            return Errors.Gallery.SubGalleryShortcutKeyDuplicated(shortcutKey);
        }

        var subGallery = SubGallery.Create(
            name,
            shortcutKey,
            cronSchedule);
        if (subGallery.IsError)
        {
            return subGallery.Errors;
        }

        _subGalleries.Add(subGallery.Value);

        return Unit.Value;
    }

    public ErrorOr<Unit> DeleteSubGallery(
        SubGalleryId subGalleryId)
    {
        var subGallery = _subGalleries.SingleOrDefault(sg => sg.Id == subGalleryId);
        if (subGallery is null)
        {
            return Errors.Gallery.SubGalleryNotFound(subGalleryId);
        }

        _subGalleries.Remove(subGallery);

        return Unit.Value;
    }

    public ErrorOr<SubGallery> GetSubGallery(
        SubGalleryId subGalleryId)
    {
        var subGallery = _subGalleries.SingleOrDefault(sg => sg.Id == subGalleryId);
        if (subGallery is null)
        {
            return Errors.Gallery.SubGalleryNotFound(subGalleryId);
        }

        return subGallery;
    }

    public ErrorOr<Unit> SetSubGalleryId(SubGalleryId subGalleryId)
    {
        if (_subGalleries.Any(sg => sg.Id == subGalleryId) is false)
        {
            return Errors.Gallery.SubGalleryNotFound(SubGalleryId);
        }

        SubGalleryId = subGalleryId;

        return Unit.Value;
    }
}

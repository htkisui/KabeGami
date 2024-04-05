using ErrorOr;
using KabeGami.Domain.Common.Errors;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Images.Enumerations;
using KabeGami.Domain.Images.Events;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Domain.Images;
public sealed class Image : AggregateRoot<ImageId>
{
    private readonly List<ImageId> _imageAlterIds = [];

    public ImageExtension ImageExtension { get; private set; }
    public ImageCategory ImageCategory { get; private set; }
    public bool IsSFW { get; private set; }
    public IReadOnlyList<ImageId> ImageAlterIds => _imageAlterIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Image(
        ImageId imageId,
        ImageExtension imageExtension,
        ImageCategory imageCategory,
        bool isSFW,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(imageId)
    {
        ImageExtension = imageExtension;
        ImageCategory = imageCategory;
        IsSFW = isSFW;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

#pragma warning disable CS8618
    private Image() { }
#pragma warning restore CS8618

    public static ErrorOr<Image> Create(
        string imageExtensionString,
        string imageCategoryString,
        bool isSFW)
    {
        var imageExtension = ImageExtension.FromName(imageExtensionString);
        if (imageExtension is null)
        {
            return Errors.Image.FileExtension.InvalidFileExtension;
        }

        var imageCategory = ImageCategory.FromName(imageCategoryString);
        if (imageCategory is null)
        {
            return Errors.Image.ImageCategory.InvalidImageCategory;
        }

        var imageId = ImageId.CreateUnique();

        var image = new Image(
            imageId,
            imageExtension,
            imageCategory,
            isSFW,
            DateTime.Now,
            DateTime.Now);

        image.AddDomainEvent(new ImageCreatedDomainEvent(Guid.NewGuid(), imageId));

        return image;
    }
}

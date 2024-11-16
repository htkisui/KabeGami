using ErrorOr;
using KabeGami.Domain.Common.Errors;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Images.Enumerations;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Domain.Images;
public sealed class Image : AggregateRoot<ImageId>
{
    public ImageExtension ImageExtension { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Image(
        ImageId imageId,
        ImageExtension imageExtension,
        DateTime createdDateTime,
        DateTime updatedDateTime) 
        : base(imageId)
    {
        ImageExtension = imageExtension;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

#pragma warning disable CS8618
    private Image() { }
#pragma warning restore CS8618

    public static ErrorOr<Image> Create(
        string imageExtensionName)
    {
        if (imageExtensionName.StartsWith('.'))
        {
            imageExtensionName = imageExtensionName[1..];
        }
        var imageExtension = ImageExtension.FromName(imageExtensionName);
        if (imageExtension is null)
        {
            return Errors.Image.ImageExtension.InvalidImageExtension(imageExtensionName);
        }

        return new Image(
            ImageId.CreateUnique(),
            imageExtension,
            DateTime.Now,
            DateTime.Now);
    }
}

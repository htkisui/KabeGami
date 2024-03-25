using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Images.Enumerations;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Domain.Images;
public sealed class Image : AggregateRoot<ImageId>
{
    public string FileName { get; }
    public FileExtension FileExtension {  get; }
    public ImageCategory ImageCategory { get; }
    public bool IsSFW { get; }
    public List<ImageId> ImageAlterIds { get; } = [];

    private Image(
        ImageId imageId,
        string fileName,
        FileExtension fileExtension,
        ImageCategory imageCategory,
        bool isSFW)
        : base(imageId)
    {
        FileName = fileName;
        FileExtension = fileExtension;
        ImageCategory = imageCategory;
        IsSFW = isSFW;
    }

    public static Image Create(
        string fileName,
        string fileExtensionString,
        string imageCategoryString,
        bool isSFW)
    {
        var fileExtension = FileExtension.FromName(fileExtensionString);
        if (fileExtension is null)
        {
            throw new Exception();
        }

        var imageCategory = ImageCategory.FromName(imageCategoryString);
        if (imageCategory is null)
        {
            throw new Exception();
        }


        return new(
            ImageId.CreateUnique(),
            fileName,
            fileExtension,
            imageCategory,
            isSFW);
    }
}

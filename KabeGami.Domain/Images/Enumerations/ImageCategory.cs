using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Images.Enumerations;
public sealed class ImageCategory : Enumeration<ImageCategory>
{
    public static readonly ImageCategory Profile = new(nameof(Profile));
    public static readonly ImageCategory CgOriginal = new(nameof(CgOriginal));
    public static readonly ImageCategory CgAlter = new(nameof(CgAlter));
    public static readonly ImageCategory Tokuten = new(nameof(Tokuten));

    public string DirectoryPath { get; private set; }
    private static int index = 1;
    private ImageCategory(string name)
        : base(name, index)
    {
        DirectoryPath = $"Wallpapers/{name}/";
        index += 1;
    }

#pragma warning disable CS8618
    private ImageCategory()
    {
    }
#pragma warning restore CS8618
}

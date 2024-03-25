using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Images.Enumerations;
public sealed class ImageCategory : Enumeration<ImageCategory>
{
    public static readonly ImageCategory Profile = new(nameof(Profile));
    public static readonly ImageCategory CgOriginal = new(nameof(CgOriginal));
    public static readonly ImageCategory CgAlter = new(nameof(CgAlter));

    public string FolderPath { get; }
    private static int index = 1;
    private ImageCategory(string name) 
        : base(name, index)
    {
        FolderPath = $"{name}/";
        index += 1;
    }
}

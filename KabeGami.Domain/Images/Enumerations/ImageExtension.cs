using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Images.Enumerations;
public sealed class ImageExtension : Enumeration<ImageExtension>
{
    public static readonly ImageExtension Jpg = new(nameof(Jpg));
    public static readonly ImageExtension Jpeg = new(nameof(Jpeg));
    public static readonly ImageExtension Png = new(nameof(Png));

    public string Extension { get; private set; }
    private static int index = 1;

    private ImageExtension(string name) : base(name, index)
    {
        Extension = $".{name.ToLower()}";
        index += 1;
    }

#pragma warning disable CS8618
    private ImageExtension()
    {
    }
#pragma warning restore CS8618
}

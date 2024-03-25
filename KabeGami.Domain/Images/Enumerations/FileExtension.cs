using KabeGami.Domain.Common.Primitives;

namespace KabeGami.Domain.Images.Enumerations;

public sealed class FileExtension: Enumeration<FileExtension>
{
    public static readonly FileExtension Jpg = new(nameof(Jpg));
    public static readonly FileExtension Jpeg = new(nameof(Jpeg));
    public static readonly FileExtension Png = new(nameof(Png));

    public string Extension { get; }
    private static int index = 1;

    private FileExtension(string name) 
        : base(name, index)
    {
        Extension = $".{name}";
        index += 1;
    }
}
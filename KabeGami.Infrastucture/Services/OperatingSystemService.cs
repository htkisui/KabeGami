using ErrorOr;
using KabeGami.Application.Common.Errors.Services;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Results;
using MediatR;

namespace KabeGami.Infrastucture.Services;
internal sealed class OperatingSystemService
    : IOperatingSystemService
{
    private readonly string wallpapersDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wallpapers/");
    private readonly string trashDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Trash/");

    public async Task<ErrorOr<Unit>> DeleteImage(
        ImageResult imageResult,
        CancellationToken cancellationToken)
    {
        try
        {
            string imagePath = Path.Combine(wallpapersDirectoryPath, imageResult.ImageNameWithExtension);
            if (File.Exists(imagePath) is false)
            {
                return Errors.OperatingSystemService.ImageAccessFailed;
            }

            if (Directory.Exists(trashDirectoryPath) is false)
            {
                Directory.CreateDirectory(trashDirectoryPath);
            }
            string imageDeletedPath = Path.Combine(trashDirectoryPath, imageResult.ImageNameWithExtension);

            await Task.Run(() => File.Move(imagePath, imageDeletedPath), cancellationToken);

            return Unit.Value;
        }
        catch
        {
            return Errors.OperatingSystemService.DeleteImageFailed;
        }
    }

    public async Task EmptyImageTrash(CancellationToken cancellationToken)
    {
        if (Directory.Exists(trashDirectoryPath) is false)
        {
            Directory.CreateDirectory(trashDirectoryPath);
        }
        await Task.Run(() =>
        {
            foreach (string image in Directory.GetFiles(trashDirectoryPath))
            {
                File.Delete(image);
            }
        }, cancellationToken);
    }

    public async Task<ErrorOr<List<string>>> GetImagePathsFromDirectory(
        string directoryPath,
        CancellationToken cancellationToken)
    {
        try
        {
            string[] imagePaths = Directory.GetFiles(directoryPath);
            return await Task.Run(() => imagePaths.ToList(), cancellationToken);
        }
        catch
        {
            return Errors.OperatingSystemService.DirectoryAccessFailed;
        }
    }

    public async Task<ErrorOr<Unit>> MoveImage(
        ImageResult imageResult,
        string imagePath,
        CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(imagePath) is false)
            {
                return Errors.OperatingSystemService.ImageAccessFailed;
            }

            if (Directory.Exists(wallpapersDirectoryPath) is false)
            {
                Directory.CreateDirectory(wallpapersDirectoryPath);
            }

            await Task.Run(() => File.Move(imagePath,
                                           Path.Combine(wallpapersDirectoryPath, imageResult.ImageNameWithExtension),
                                           true), cancellationToken);

            return Unit.Value;
        }
        catch
        {
            return Errors.OperatingSystemService.MoveImageFailed;
        }
    }
}

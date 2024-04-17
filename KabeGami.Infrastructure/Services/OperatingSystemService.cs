using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Common.Results;
using MediatR;

namespace KabeGami.Infrastructure.Services;
internal sealed class OperatingSystemService : IOperatingSystemService
{
    private static async Task<ErrorOr<string>> CreateDirectoryHierarchy(
        string imageCategoryDirectoryPath)
    {
        try
        {
            string categoryDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageCategoryDirectoryPath);
            if (Directory.Exists(categoryDirectoryPath) is false)
            {
                Directory.CreateDirectory(categoryDirectoryPath);
            }

            return await Task.FromResult(categoryDirectoryPath);
        }
        catch
        {
            return Errors.OperatingSystemService.CreateDirectoryFailed;
        }
    }

    public async Task<ErrorOr<Unit>> DeleteImage(
        ImageResult imageResult, 
        CancellationToken cancellationToken)
    {
        try
        {
            var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{imageResult.CategoryDirectoryPath}{imageResult.ImageGuidResult}{imageResult.Extension}");
            if (File.Exists(imagePath) is false)
            {
                return Errors.OperatingSystemService.ImageAccessFailed;
            }

            await Task.Run(() => File.Delete(imagePath), cancellationToken);

            return Unit.Value;
        }
        catch
        {
            return Errors.OperatingSystemService.DeleteImageFailed;
        }
    }

    public async Task<ErrorOr<List<string>>> GetFilesFromDirectory(
        string directoryPath,
        CancellationToken cancellationToken)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath);
            return await Task.Run(() => files.ToList(), cancellationToken);
        }
        catch
        {
            return Errors.OperatingSystemService.DirectoryAccessFailed;
        }
    }

    public async Task<ErrorOr<Unit>> MoveImage(
        ImageResult imageResult,
        string imageSourcePath, 
        CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(imageSourcePath) is false)
            {
                return Errors.OperatingSystemService.ImageAccessFailed;
            }

            var categoryDirectoryPath = await CreateDirectoryHierarchy(imageResult.CategoryDirectoryPath);
            if (categoryDirectoryPath.IsError)
            {
                return categoryDirectoryPath.Errors;
            }

            var imageDestinationPath = Path.Combine(categoryDirectoryPath.Value, $"{imageResult.ImageGuidResult}{imageResult.Extension}");
            await Task.Run(() => File.Move(imageSourcePath, imageDestinationPath, true), cancellationToken);

            return Unit.Value;
        }
        catch
        {
            return Errors.OperatingSystemService.MoveImageFailed;
        }
    }

}

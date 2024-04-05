using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Common;
using MediatR;

namespace KabeGami.Infrastructure.Services;
internal sealed class OperatingSystemService : IOperatingSystemService
{
    public async Task<ErrorOr<Unit>> MoveImage(
        ImageResult imageResult,
        string imageSourcePath)
    {
        try
        {
            if (!File.Exists(imageSourcePath))
            {
                return Errors.OperatingSystemService.ImageSourceAccessFailed;
            }

            var categoryDirectoryPath = await CreateDirectoryHierarchy(imageResult.CategoryDirectoryPath);
            if (categoryDirectoryPath.IsError)
            {
                return categoryDirectoryPath.Errors;
            }

            var imageDestinationPath = Path.Combine(categoryDirectoryPath.Value, $"{imageResult.Id}{imageResult.Extension}");
            File.Move(imageSourcePath, imageDestinationPath, true);

            return Unit.Value;
        }
        catch
        {
            return Errors.OperatingSystemService.MoveImageFailed;
        }
    }

    private static async Task<ErrorOr<string>> CreateDirectoryHierarchy(string imageCategoryDirectoryPath)
    {
        try
        {
            string categoryDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageCategoryDirectoryPath);
            if (!Directory.Exists(categoryDirectoryPath))
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

    public async Task<ErrorOr<List<string>>> GetFilesFromDirectory(string directoryPath)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath);
            return await Task.FromResult(files.ToList());
        }
        catch
        {
            return Errors.OperatingSystemService.DirectoryAccessFailed;
        }
    }
}

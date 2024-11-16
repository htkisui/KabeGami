using ErrorOr;
using KabeGami.Application.Images.Results;
using MediatR;

namespace KabeGami.Application.Common.Interfaces.Services;
public interface IOperatingSystemService
{
    Task<ErrorOr<Unit>> DeleteImage(ImageResult imageResult, CancellationToken cancellationToken);
    Task EmptyImageTrash(CancellationToken cancellationToken);
    Task<ErrorOr<List<string>>> GetImagePathsFromDirectory(string directoryPath, CancellationToken cancellationToken);
    Task<ErrorOr<Unit>> MoveImage(ImageResult imageResult, string imagePath, CancellationToken cancellationToken);
}

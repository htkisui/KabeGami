using ErrorOr;
using KabeGami.Application.Images.Common.Results;
using MediatR;

namespace KabeGami.Application.Common.Interfaces.Services;
public interface IOperatingSystemService
{
    Task<ErrorOr<Unit>> DeleteImage(ImageResult imageResult, CancellationToken cancellationToken);
    Task<ErrorOr<List<string>>> GetFilesFromDirectory(string directoryPath, CancellationToken cancellationToken);
    Task<ErrorOr<Unit>> MoveImage(ImageResult imageResult, string imageSourcePath, CancellationToken cancellationToken);
}

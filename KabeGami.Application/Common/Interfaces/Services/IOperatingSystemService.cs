using ErrorOr;
using KabeGami.Application.Images.Common;
using MediatR;

namespace KabeGami.Application.Common.Interfaces.Services;
public interface IOperatingSystemService
{
    Task<ErrorOr<List<string>>> GetFilesFromDirectory(string directoryPath);
    Task<ErrorOr<Unit>> MoveImage(ImageResult imageResult, string imageSourcePath);
}

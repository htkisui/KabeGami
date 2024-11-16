using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImagePathsFromDirectory;
internal sealed class GetImagePathsFromDirectoryQueryHandler(
    IOperatingSystemService operatingSystemService)
        : IRequestHandler<GetImagePathsFromDirectoryQuery, ErrorOr<List<string>>>
{
    private IOperatingSystemService _operatingSystemService = operatingSystemService;

    public async Task<ErrorOr<List<string>>> Handle(GetImagePathsFromDirectoryQuery request, CancellationToken cancellationToken)
    {
        var imagePaths = await _operatingSystemService.GetImagePathsFromDirectory(
            request.Payload.DirectoryPath,
            cancellationToken);
        if (imagePaths.IsError)
        {
            return imagePaths.Errors;
        }

        return imagePaths;
    }
}

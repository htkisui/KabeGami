using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Domain.Images.Enumerations;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImagesFromDirectory;
internal sealed class GetImageNamesFromDirectoryQueryHandler(
    IOperatingSystemService operatingSystemService)
        : IRequestHandler<GetImageNamesFromDirectoryQuery, ErrorOr<List<string>>>
{
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;

    public async Task<ErrorOr<List<string>>> Handle(GetImageNamesFromDirectoryQuery request, CancellationToken cancellationToken)
    {
        var files = await _operatingSystemService.GetFilesFromDirectory(
            request.DirectoryPath,
            cancellationToken);
        if (files.IsError)
        {
            return files.Errors;
        }

        var imageNames = new List<string>();
        foreach (var file in files.Value)
        {
            var fileExtension = Path.GetExtension(file)[1..]; // Get extension without dot.
            var imageExtension = ImageExtension.FromName(fileExtension);
            if (imageExtension is not null)
            {
                imageNames.Add(file);
            }
        }

        return imageNames;
    }
}

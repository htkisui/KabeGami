using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Domain.Images.Enumerations;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImagesFromDirectory;
internal class GetImagesFromDirectoryQueryHandler(
    IOperatingSystemService operatingSystemService)
    : IRequestHandler<GetImagesFromDirectoryQuery, ErrorOr<List<string>>>
{
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;

    public async Task<ErrorOr<List<string>>> Handle(GetImagesFromDirectoryQuery request, CancellationToken cancellationToken)
    {
        var files = await _operatingSystemService.GetFilesFromDirectory(request.DirectoryPath);
        if (files.IsError)
        {
            return files.Errors;
        }

        var images = new List<string>();
        foreach (var file in files.Value)
        {
            var fileExtension = Path.GetExtension(file)[1..]; // Get extension without dot.
            var imageExtension = ImageExtension.FromName(fileExtension);
            if (imageExtension is not null)
            {
                images.Add(file);
            }
        }

        return images;
    }
}

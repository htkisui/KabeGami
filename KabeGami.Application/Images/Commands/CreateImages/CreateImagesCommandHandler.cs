using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Commands.CreateImage;
using KabeGami.Application.Images.Results;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImages;
internal sealed class CreateImagesCommandHandler(
    IOperatingSystemService operatingSystemService,
    ISender sender)
        : IRequestHandler<CreateImagesCommand, ImagesCreatedResult>
{
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;
    private readonly ISender _sender = sender;

    public async Task<ImagesCreatedResult> Handle(CreateImagesCommand request, CancellationToken cancellationToken)
    {
        List<Error> errors = [];
        List<ImageResult> images = [];

        foreach (var imagePath in request.Payload.ImagePaths)
        {
            var command = new CreateImageCommand(imagePath);
            var res = await _sender.Send(command);
            if (res.IsError)
            {
                errors.Add(res.FirstError);
            }
            else
            {
                images.Add(res.Value);
            }
        }

        ImagesCreatedResult imagesCreatedResult = new(images, errors);

        return imagesCreatedResult;
    }
}

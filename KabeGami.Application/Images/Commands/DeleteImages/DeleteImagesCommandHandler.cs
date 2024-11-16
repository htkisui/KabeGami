using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Commands.DeleteImage;
using MediatR;

namespace KabeGami.Application.Images.Commands.DeleteImages;
internal sealed class DeleteImagesCommandHandler(
    IOperatingSystemService operatingSystemService,
    ISender sender)
        : IRequestHandler<DeleteImagesCommand, ErrorOr<Unit>>
{
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;
    private readonly ISender _sender = sender;

    public async Task<ErrorOr<Unit>> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {
        List<Error> errors = [];

        foreach (var imageGuid in request.Payload.ImageGuids)
        {
            var command = new DeleteImageCommand(imageGuid);
            var res = await _sender.Send(command);
            if (res.IsError)
            {
                errors.Add(res.FirstError);
            }
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return Unit.Value;
    }
}

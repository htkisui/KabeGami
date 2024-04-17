using ErrorOr;
using KabeGami.Application.Images.Commands.CreateImage;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImagesCommand;
internal sealed class CreateImagesCommandHandler(
    ISender sender)
        : IRequestHandler<CreateImagesCommand, ErrorOr<Unit>>
{
    private readonly ISender _sender = sender;

    public async Task<ErrorOr<Unit>> Handle(CreateImagesCommand request, CancellationToken cancellationToken)
    {
        List<Error> errors = [];

        foreach (var imageSourcePath in request.ImageSourcePaths)
        {
            var command = new CreateImageCommand(
                imageSourcePath,
                request.ImageCategory,
                request.IsSFW);

            var res = await _sender.Send(command, cancellationToken);
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

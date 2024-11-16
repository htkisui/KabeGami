using ErrorOr;
using KabeGami.Application.Images.Requests;
using MediatR;

namespace KabeGami.Application.Images.Commands.DeleteImages;
public sealed record DeleteImagesCommand(
    DeleteImagesRequest Payload)
        : IRequest<ErrorOr<Unit>>;

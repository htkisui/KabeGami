using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Commands.DeleteImage;
internal sealed record DeleteImageCommand(
    Guid ImageGuid)
        : IRequest<ErrorOr<Unit>>;

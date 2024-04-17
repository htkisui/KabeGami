using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Commands.DeleteImage;
public sealed record DeleteImageCommand(
    Guid ImageGuid) : IRequest<ErrorOr<Unit>>;

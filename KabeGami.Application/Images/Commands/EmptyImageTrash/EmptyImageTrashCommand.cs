using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Commands.EmptyImageTrash;
public sealed class EmptyImageTrashCommand
    : IRequest<ErrorOr<Unit>>;

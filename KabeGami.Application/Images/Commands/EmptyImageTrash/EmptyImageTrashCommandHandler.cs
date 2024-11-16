using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using MediatR;

namespace KabeGami.Application.Images.Commands.EmptyImageTrash;
internal sealed class EmptyImageTrashCommandHandler(
    IOperatingSystemService operatingSystemService)
        : IRequestHandler<EmptyImageTrashCommand, ErrorOr<Unit>>
{
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;

    public async Task<ErrorOr<Unit>> Handle(EmptyImageTrashCommand request, CancellationToken cancellationToken)
    {
        await _operatingSystemService.EmptyImageTrash(cancellationToken);
        return Unit.Value;
    }
}

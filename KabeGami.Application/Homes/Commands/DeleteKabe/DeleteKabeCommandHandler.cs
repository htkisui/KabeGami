using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Homes.ValueObjects;
using MediatR;

namespace KabeGami.Application.Homes.Commands.DeleteKabe;
internal sealed class DeleteKabeCommandHandler(
    IHomeRepository homeRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteKabeCommand, ErrorOr<Unit>>
{
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(DeleteKabeCommand request, CancellationToken cancellationToken)
    {
        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var kabeId = KabeId.Create(request.Payload.KabeGuid);
        var kabeDeleted = home.Value.DeleteKabe(kabeId);
        if (kabeDeleted.IsError)
        {
            return kabeDeleted.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

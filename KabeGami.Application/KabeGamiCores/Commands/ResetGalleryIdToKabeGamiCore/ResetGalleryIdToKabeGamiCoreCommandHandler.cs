using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.ResetGalleryIdToKabeGamiCore;
internal sealed class ResetGalleryIdToKabeGamiCoreCommandHandler(
    IKabeGamiCoreRepository kabeGamiCoreRepository, 
    IUnitOfWork unitOfWork)
        : IRequestHandler<ResetGalleryIdToKabeGamiCoreCommand, ErrorOr<Unit>>
{
    private readonly IKabeGamiCoreRepository _kabeGamiCoreRepository = kabeGamiCoreRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ErrorOr<Unit>> Handle(ResetGalleryIdToKabeGamiCoreCommand request, CancellationToken cancellationToken)
    {
        var kabeGamiCore = await _kabeGamiCoreRepository.GetKabeGamiCoreAsync(cancellationToken);
        if (kabeGamiCore.IsError)
        {
            return kabeGamiCore.Errors;
        }

        kabeGamiCore.Value.ResetGalleryId();

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

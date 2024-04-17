using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.SetGalleryIdToKabeGamiCore;
internal sealed class SetGalleryIdToKabeGamiCoreCommandHandler(
    IGalleryRepository galleryRepository,
    IKabeGamiCoreRepository kabeGamiCoreRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<SetGalleryIdToKabeGamiCoreCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IKabeGamiCoreRepository _kabeGamiCoreRepository = kabeGamiCoreRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(SetGalleryIdToKabeGamiCoreCommand request, CancellationToken cancellationToken)
    {
        var kabeGamiCore = await _kabeGamiCoreRepository.GetKabeGamiCoreAsync(cancellationToken);
        if (kabeGamiCore.IsError)
        {
            return kabeGamiCore.Errors;
        }

        var galleryId = GalleryId.Create(request.GalleryGuid);
        var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        kabeGamiCore.Value.SetGalleryId(galleryId);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

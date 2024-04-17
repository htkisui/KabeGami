using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.KabeGamiCores.Commands.ResetGalleryIdToKabeGamiCore;
using KabeGami.Domain.Galleries.DomainEvents;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.DomainEventHandlers;
internal sealed class UpdateKabeGamiCoreAfterGalleryDeletedDomainEventHandler(
    IKabeGamiCoreRepository kabeGamiCoreRepository,
    ISender sender)
        : INotificationHandler<GalleryDeletedDomainEvent>
{
    private readonly IKabeGamiCoreRepository _kabeGamiCoreRepository = kabeGamiCoreRepository;
    private readonly ISender _sender = sender;

    public async Task Handle(GalleryDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var kabeGamiCore = await _kabeGamiCoreRepository.GetKabeGamiCoreAsync(cancellationToken);
        if (kabeGamiCore.IsError)
        {
            return;
        }

        if (kabeGamiCore.Value.HasGalleryId(notification.GalleryId))
        {
            var command = new ResetGalleryIdToKabeGamiCoreCommand();
            await _sender.Send(command, cancellationToken);
        }
    }
}

using KabeGami.Domain.Images.Events;
using MediatR;

namespace KabeGami.Application.Images.Events;
internal sealed class ImageCreatedDomainEventHandler
    : INotificationHandler<ImageCreatedDomainEvent>
{
    public async Task Handle(ImageCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}

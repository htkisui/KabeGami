using KabeGami.Domain.Images;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KabeGami.Infrastructure.Persistence.Interceptors;
public sealed class PublishDomainEventsInterceptor(
    IPublisher publisher)
    : SaveChangesInterceptor
{
    private readonly IPublisher _publisher = publisher;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? context)
    {
        if (context is null) return;

        var entitiesWithDomainEvents = context.ChangeTracker.Entries<Image>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();

        var domainEvents = entitiesWithDomainEvents.SelectMany(entry => entry.DomainEvents).ToList();

        entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}

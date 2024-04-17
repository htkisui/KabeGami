using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;

namespace KabeGami.Domain.Galleries.DomainEvents;
public sealed record GalleryDeletedDomainEvent(
    Guid Guid,
    GalleryId GalleryId) : DomainEvent(Guid);
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Domain.Images.DomainEvents;
public sealed record ImageCreatedDomainEvent(
    Guid Guid,
    ImageId ImageId) : DomainEvent(Guid);

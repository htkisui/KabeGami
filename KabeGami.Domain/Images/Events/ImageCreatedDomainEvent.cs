using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Domain.Images.Events;
public record ImageCreatedDomainEvent(
    Guid Id,
    ImageId ImageId) 
    : DomainEvent(Id);

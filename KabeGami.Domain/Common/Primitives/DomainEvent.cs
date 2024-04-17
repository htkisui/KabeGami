using MediatR;

namespace KabeGami.Domain.Common.Primitives;
public record DomainEvent(Guid Guid) : INotification;

using MediatR;

namespace KabeGami.Domain.Common.Primitives;
public record DomainEvent(Guid Id) : INotification;

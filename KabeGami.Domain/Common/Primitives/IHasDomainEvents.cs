using MediatR;

namespace KabeGami.Domain.Common.Primitives;
public interface IHasDomainEvents : INotification
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
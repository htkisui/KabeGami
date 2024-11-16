using ErrorOr;
using KabeGami.Application.Homes.Requests;
using MediatR;

namespace KabeGami.Application.Homes.Commands.DeleteKabe;
public sealed record DeleteKabeCommand(
    DeleteKabeRequest Payload)
        : IRequest<ErrorOr<Unit>>;

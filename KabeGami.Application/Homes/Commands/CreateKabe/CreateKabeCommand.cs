using ErrorOr;
using KabeGami.Application.Homes.Requests;
using KabeGami.Application.Homes.Results;
using MediatR;

namespace KabeGami.Application.Homes.Commands.CreateKabe;
public sealed record CreateKabeCommand(
    CreateKabeRequest Payload)
        : IRequest<ErrorOr<KabeResult>>;

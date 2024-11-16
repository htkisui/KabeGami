using ErrorOr;
using KabeGami.Application.Homes.Requests;
using KabeGami.Application.Homes.Results;
using MediatR;

namespace KabeGami.Application.Homes.Commands.SetDefaultKabe;
public sealed record SetDefaultKabeCommand(
    SetDefaultKabeRequest Payload)
        : IRequest<ErrorOr<HomeResult>>;

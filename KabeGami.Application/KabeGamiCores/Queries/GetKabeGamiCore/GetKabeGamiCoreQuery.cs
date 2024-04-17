using ErrorOr;
using KabeGami.Application.KabeGamiCores.Common.Results;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Queries.GetKabeGamiCore;
public sealed record GetKabeGamiCoreQuery : IRequest<ErrorOr<KabeGamiCoreResult>>;

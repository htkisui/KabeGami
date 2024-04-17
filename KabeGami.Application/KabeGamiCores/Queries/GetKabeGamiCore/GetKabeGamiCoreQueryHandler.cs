using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.KabeGamiCores.Common.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Queries.GetKabeGamiCore;
internal sealed class GetKabeGamiCoreQueryHandler(
    IKabeGamiCoreRepository kabeGamiCoreRepository, 
    IMapper mapper)
        : IRequestHandler<GetKabeGamiCoreQuery, ErrorOr<KabeGamiCoreResult>>
{
    private readonly IKabeGamiCoreRepository _kabeGamiCoreRepository = kabeGamiCoreRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<KabeGamiCoreResult>> Handle(GetKabeGamiCoreQuery request, CancellationToken cancellationToken)
    {
        var kabeGamiCore = await _kabeGamiCoreRepository.GetKabeGamiCoreAsync(cancellationToken);
        if (kabeGamiCore.IsError)
        {
            return kabeGamiCore.Errors;
        }

        var kabeGamiCoreResult = _mapper.Map<KabeGamiCoreResult>(kabeGamiCore.Value);

        return kabeGamiCoreResult;
    }
}

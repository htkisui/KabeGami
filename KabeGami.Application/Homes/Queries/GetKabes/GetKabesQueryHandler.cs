using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Homes.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Homes.Queries.GetKabes;
internal sealed class GetKabesQueryHandler(
    IHomeRepository homeRepository, IMapper mapper)
        : IRequestHandler<GetKabesQuery, ErrorOr<List<KabeResult>>>
{
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<List<KabeResult>>> Handle(GetKabesQuery request, CancellationToken cancellationToken)
    {
        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var kabeResults = home.Value.Kabes.Select(_mapper.Map<KabeResult>).ToList();

        return kabeResults;
    }
}

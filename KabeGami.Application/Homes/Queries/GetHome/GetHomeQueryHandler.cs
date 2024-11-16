using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Homes.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Homes.Queries.GetHome;
internal sealed class GetHomeQueryHandler(
    IHomeRepository homeRepository, 
    IMapper mapper)
        : IRequestHandler<GetHomeQuery, ErrorOr<HomeResult>>
{
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<HomeResult>> Handle(GetHomeQuery request, CancellationToken cancellationToken)
    {
        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var homeResult = _mapper.Map<HomeResult>(home.Value);

        return homeResult;
    }
}

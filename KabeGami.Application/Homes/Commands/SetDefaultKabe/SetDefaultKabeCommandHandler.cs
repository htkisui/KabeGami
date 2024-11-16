using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Homes.Results;
using KabeGami.Domain.Homes.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Homes.Commands.SetDefaultKabe;
internal sealed class SetDefaultKabeCommandHandler(
    IHomeRepository homeRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
        : IRequestHandler<SetDefaultKabeCommand, ErrorOr<HomeResult>>
{
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ErrorOr<HomeResult>> Handle(SetDefaultKabeCommand request, CancellationToken cancellationToken)
    {
        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var kabeId = KabeId.Create(request.Payload.KabeGuid);
        var defaultKabe = home.Value.SetDefaultKabe(kabeId);
        if (defaultKabe.IsError)
        {
            return defaultKabe.Errors;
        }

        var homeResult = _mapper.Map<HomeResult>(home.Value);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return homeResult;
    }
}

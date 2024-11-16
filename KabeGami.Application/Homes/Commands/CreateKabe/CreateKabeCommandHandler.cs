using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Homes.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Homes.Commands.CreateKabe;
internal sealed class CreateKabeCommandHandler(
    IGalleryRepository galleryRepository,
    IHomeRepository homeRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
        : IRequestHandler<CreateKabeCommand, ErrorOr<KabeResult>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<KabeResult>> Handle(CreateKabeCommand request, CancellationToken cancellationToken)
    {
        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var gallery = await _galleryRepository.GetGalleryByNameAsync(request.Payload.GalleryName, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var kabe = home.Value.CreateKabe(
            request.Payload.Name,
            request.Payload.Combination,
            request.Payload.CronSchedule,
            gallery.Value.Id);
        if (kabe.IsError)
        {
            return kabe.Errors;
        }

        var kabeResult = _mapper.Map<KabeResult>(kabe.Value);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return kabeResult;
    }
}

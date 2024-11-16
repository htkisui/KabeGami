using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Galleries.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleryNames;
internal sealed class GetGalleryNamesQueryHandler(
    IGalleryRepository galleryRepository,
    IMapper mapper)
        : IRequestHandler<GetGalleryNamesQuery, ErrorOr<List<GalleryNameResult>>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<ErrorOr<List<GalleryNameResult>>> Handle(GetGalleryNamesQuery request, CancellationToken cancellationToken)
    {
        var galleries = await _galleryRepository.GetGalleriesAsync(cancellationToken);

        var galleryNameResults = galleries.Select(_mapper.Map<GalleryNameResult>).ToList();

        return galleryNameResults;
    }
}

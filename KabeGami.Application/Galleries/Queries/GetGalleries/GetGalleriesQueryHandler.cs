using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Galleries.Common.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleries;
internal sealed class GetGalleriesQueryHandler(
    IGalleryRepository galleryRepository, 
    IMapper mapper)
        : IRequestHandler<GetGalleriesQuery, ErrorOr<List<GalleryResult>>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<List<GalleryResult>>> Handle(GetGalleriesQuery request, CancellationToken cancellationToken)
    {
        var galleries = await _galleryRepository.GetGalleriesAsync(cancellationToken);

        var galleriesResult = galleries.Select(_mapper.Map<GalleryResult>).ToList();

        return galleriesResult;
    }
}

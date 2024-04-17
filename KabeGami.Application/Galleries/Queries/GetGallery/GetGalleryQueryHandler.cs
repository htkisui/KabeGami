using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Galleries.Common.Results;
using KabeGami.Domain.Galleries.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGallery;
internal sealed class GetGalleryQueryHandler(
    IGalleryRepository galleryRepository, 
    IMapper mapper)
        : IRequestHandler<GetGalleryQuery, ErrorOr<GalleryResult>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<GalleryResult>> Handle(GetGalleryQuery request, CancellationToken cancellationToken)
    {
        var galleryId = GalleryId.Create(request.GalleryGuid);
        var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var galleryResult = _mapper.Map<GalleryResult>(gallery.Value);

        return galleryResult;
    }
}

using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleryGuids;
internal sealed class GetGalleryGuidsQueryHandler(
    IGalleryRepository galleryRepository)
        : IRequestHandler<GetGalleryGuidsQuery, ErrorOr<List<Guid>>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;

    public async Task<ErrorOr<List<Guid>>> Handle(GetGalleryGuidsQuery request, CancellationToken cancellationToken)
    {
        var galleryIds = await _galleryRepository.GetGalleryIdsAsync(cancellationToken);
        var galleryGuids = galleryIds.Select(gi => gi.Value).ToList();

        return galleryGuids;
    }
}

using ErrorOr;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.AddImageIdsToSubGallery;
public sealed record AddImageIdsToSubGalleryCommand(
    Guid GalleryGuid,
    Guid SubGalleryGuid,
    List<Guid> ImageGuids) : IRequest<ErrorOr<Unit>>;


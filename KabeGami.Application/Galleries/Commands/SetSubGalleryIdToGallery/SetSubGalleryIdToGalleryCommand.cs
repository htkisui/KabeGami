using ErrorOr;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.SetSubGalleryIdToGallery;
public sealed record SetSubGalleryIdToGalleryCommand(
    Guid GalleryGuid,
    Guid SubGalleryGuid) : IRequest<ErrorOr<Unit>>;

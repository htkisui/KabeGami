using ErrorOr;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.DeleteSubGallery;
public sealed record DeleteSubGalleryCommand(
    Guid GalleryGuid,
    Guid SubGalleryGuid) : IRequest<ErrorOr<Unit>>;

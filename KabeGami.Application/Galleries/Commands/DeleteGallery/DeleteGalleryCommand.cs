using ErrorOr;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.DeleteGallery;
public sealed record DeleteGalleryCommand(
    Guid GalleryGuid)
        : IRequest<ErrorOr<Unit>>;

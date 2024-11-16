using ErrorOr;
using KabeGami.Application.Galleries.Requests;
using KabeGami.Application.Galleries.Results;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.UpdateGalleryImages;
public sealed record UpdateGalleryImagesCommand(
    UpdateGalleryImagesQuery Payload)
        : IRequest<ErrorOr<GalleryResult>>;

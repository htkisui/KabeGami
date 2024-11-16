using ErrorOr;
using KabeGami.Application.Galleries.Results;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.CreateGallery;
public sealed record CreateGalleryCommand(
    string Name)
        : IRequest<ErrorOr<GalleryResult>>;

using ErrorOr;
using KabeGami.Application.Galleries.Results;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleries;
public sealed record GetGalleriesQuery
    : IRequest<ErrorOr<List<GalleryResult>>>;

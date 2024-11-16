using ErrorOr;
using KabeGami.Application.Galleries.Results;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleryNames;
public sealed record GetGalleryNamesQuery
    : IRequest<ErrorOr<List<GalleryNameResult>>>;
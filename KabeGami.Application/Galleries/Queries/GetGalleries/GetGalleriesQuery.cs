using ErrorOr;
using KabeGami.Application.Galleries.Common.Results;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleries;
public sealed class GetGalleriesQuery(
    ) : IRequest<ErrorOr<List<GalleryResult>>>;

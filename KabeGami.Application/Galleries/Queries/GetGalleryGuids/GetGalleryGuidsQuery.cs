using ErrorOr;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGalleryGuids;
public sealed record GetGalleryGuidsQuery
    : IRequest<ErrorOr<List<Guid>>>;

using ErrorOr;
using KabeGami.Application.Galleries.Common.Results;
using MediatR;

namespace KabeGami.Application.Galleries.Queries.GetGallery;
public sealed record GetGalleryQuery(
    Guid GalleryGuid) : IRequest<ErrorOr<GalleryResult>>;

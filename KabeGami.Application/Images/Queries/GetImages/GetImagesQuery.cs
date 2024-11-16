using ErrorOr;
using KabeGami.Application.Images.Results;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImages;
public sealed record GetImagesQuery
    : IRequest<ErrorOr<List<ImageResult>>>;

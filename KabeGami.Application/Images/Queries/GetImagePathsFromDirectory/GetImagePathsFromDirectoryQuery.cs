using ErrorOr;
using KabeGami.Application.Images.Requests;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImagePathsFromDirectory;
public sealed record GetImagePathsFromDirectoryQuery(
    GetImagePathsFromDirectoryRequest Payload)
        : IRequest<ErrorOr<List<string>>>;

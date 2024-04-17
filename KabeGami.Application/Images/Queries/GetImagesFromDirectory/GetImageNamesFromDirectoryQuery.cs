using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImagesFromDirectory;
public sealed record GetImageNamesFromDirectoryQuery(
    string DirectoryPath) : IRequest<ErrorOr<List<string>>>;

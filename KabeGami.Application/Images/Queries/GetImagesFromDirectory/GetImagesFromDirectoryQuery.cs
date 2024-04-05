using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImagesFromDirectory;
public record GetImagesFromDirectoryQuery(
    string DirectoryPath) : IRequest<ErrorOr<List<string>>>;

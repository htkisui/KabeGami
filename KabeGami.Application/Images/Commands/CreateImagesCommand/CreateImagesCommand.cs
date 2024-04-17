using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImagesCommand;
public sealed record CreateImagesCommand(
    List<string> ImageSourcePaths,
    string ImageCategory,
    bool IsSFW) : IRequest<ErrorOr<Unit>>;

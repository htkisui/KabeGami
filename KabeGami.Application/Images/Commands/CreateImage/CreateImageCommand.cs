using ErrorOr;
using KabeGami.Application.Images.Results;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImage;
internal sealed record CreateImageCommand(
    string ImagePath)
        : IRequest<ErrorOr<ImageResult>>;


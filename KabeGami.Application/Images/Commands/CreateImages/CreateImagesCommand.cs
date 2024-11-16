using KabeGami.Application.Images.Requests;
using KabeGami.Application.Images.Results;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImages;
public sealed record CreateImagesCommand(
    CreateImagesRequest Payload)
        : IRequest<ImagesCreatedResult>;

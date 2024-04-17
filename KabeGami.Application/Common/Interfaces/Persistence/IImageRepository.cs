using ErrorOr;
using KabeGami.Domain.Images;
using KabeGami.Domain.Images.ValueObjects;
using MediatR;

namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IImageRepository
{
    Task AddImageAsync(Image image, CancellationToken cancellationToken);
    Task<ErrorOr<List<Image>>> GetImages(List<ImageId> imageIds, CancellationToken cancellationToken);
    Task<ErrorOr<Unit>> IsImageIdsExist(List<ImageId> imageIds, CancellationToken cancellationToken);
    Task<ErrorOr<Image>> RemoveImageAsync(ImageId imageId, CancellationToken cancellationToken);
}

using ErrorOr;
using KabeGami.Domain.Images;
using KabeGami.Domain.Images.ValueObjects;
using MediatR;

namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IImageRepository
{
    Task AddImageAsync(Image image, CancellationToken cancellationToken);
    Task<List<Image>> GetImagesAsync(CancellationToken cancellationToken);
    Task<ErrorOr<List<Image>>> GetImagesByIdsAsync(List<ImageId> imageIds, CancellationToken cancellationToken);
    Task<ErrorOr<Unit>> IsImageIdsExistAsync(List<ImageId> imageIds, CancellationToken cancellationToken);
    Task<ErrorOr<Image>> RemoveImageByIdAsync(ImageId imageId, CancellationToken cancellationToken);
}

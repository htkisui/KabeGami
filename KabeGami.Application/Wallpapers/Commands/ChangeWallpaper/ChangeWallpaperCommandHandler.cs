using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Results;
using KabeGami.Domain.Galleries.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Wallpapers.Commands.ChangeWallpaper;
internal sealed class ChangeWallpaperCommandHandler(
    IGalleryRepository galleryRepository, 
    IImageRepository imageRepository,
    IMapper mapper,
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<ChangeWallpaperCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;
    public async Task<ErrorOr<Unit>> Handle(ChangeWallpaperCommand request, CancellationToken cancellationToken)
    {
        var galleryId = GalleryId.Create(request.Payload.GalleryGuid);
        var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var images = await _imageRepository.GetImagesByIdsAsync([.. gallery.Value.ImageIds], cancellationToken);
        if (images.IsError)
        {
            return images.Errors;
        }
        var imageResults = images.Value.Select(_mapper.Map<ImageResult>).ToList();

        await _wallpaperSystemService.SetWallpaperSystemJobAsync(imageResults, request.Payload.CronSchedule, cancellationToken);

        return Unit.Value;
    }
}

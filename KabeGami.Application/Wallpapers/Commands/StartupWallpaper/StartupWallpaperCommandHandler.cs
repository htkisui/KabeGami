using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Homes.Results;
using KabeGami.Application.Images.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Wallpapers.Commands.StartupWallpaper;
internal sealed class StartupWallpaperCommandHandler(
    IGalleryRepository galleryRepository,
    IHomeRepository homeRepository,
    IImageRepository imageRepository,
    IMapper mapper,
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<StartupWallpaperCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;
    public async Task<ErrorOr<Unit>> Handle(StartupWallpaperCommand request, CancellationToken cancellationToken)
    {
        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var kabe = home.Value.DefaultKabe;
        if (kabe is null)
        {
            return Unit.Value;
        }

        var gallery = await _galleryRepository.GetGalleryByIdAsync(kabe.GalleryId, cancellationToken);
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
        await _wallpaperSystemService.SetWallpaperSystemJobAsync(imageResults, kabe.CronSchedule, cancellationToken);

        return Unit.Value;
    }
}

using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Common.Results;
using KabeGami.Domain.Galleries.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.StartupWallpaper;
internal sealed class StartupWallpaperCommandHandler(
    IGalleryRepository galleryRepository,
    IKabeGamiCoreRepository kabeGamiCoreRepository,
    IImageRepository imageRepository,
    IMapper mapper,
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<StartupWallpaperCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IKabeGamiCoreRepository _kabeGamiCoreRepository = kabeGamiCoreRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;

    public async Task<ErrorOr<Unit>> Handle(StartupWallpaperCommand request, CancellationToken cancellationToken)
    {
        var kabeGamiCore = await _kabeGamiCoreRepository.GetKabeGamiCoreAsync(cancellationToken);
        if (kabeGamiCore.IsError)
        {
            return kabeGamiCore.Errors;
        }

        if (kabeGamiCore.Value.GalleryId == GalleryId.CreateEmpty())
        {
            return Errors.StartupWallpaperCommandHandler.DefaultGalleryNotFound;
        }
        var gallery = await _galleryRepository.GetGalleryByIdAsync(kabeGamiCore.Value.GalleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        if (gallery.Value.SubGalleryId == SubGalleryId.CreateEmpty())
        {
            return Errors.StartupWallpaperCommandHandler.DefaultSubGalleryNotFound;
        }
        var subGallery = gallery.Value.GetSubGallery(gallery.Value.SubGalleryId);
        if (subGallery.IsError)
        {
            return subGallery.Errors;
        }

        var images = await _imageRepository.GetImages([.. subGallery.Value.ImageIds], cancellationToken);
        if (images.IsError)
        {
            return images.Errors;
        }
        var imageResults = images.Value.Select(_mapper.Map<ImageResult>).ToList();

        var wallpaperSystemServiceResult = await _wallpaperSystemService.SetWallpaperSystemJob(imageResults, subGallery.Value.CronSchedule, cancellationToken);
        if (wallpaperSystemServiceResult.IsError)
        {
            return wallpaperSystemServiceResult.Errors;
        }

        return Unit.Value;
    }
}

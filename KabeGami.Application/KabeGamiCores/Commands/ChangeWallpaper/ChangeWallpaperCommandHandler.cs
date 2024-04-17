using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Common.Results;
using KabeGami.Domain.Galleries.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.SetWallpaper;
internal sealed class ChangeWallpaperCommandHandler(
    IGalleryRepository galleryRepository,
    IImageRepository imageRepository,
    IKabeGamiCoreRepository kabeGamiCoreRepository,
    IMapper mapper,
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<ChangeWallpaperCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IKabeGamiCoreRepository _kabeGamiCoreRepository = kabeGamiCoreRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;

    public async Task<ErrorOr<Unit>> Handle(ChangeWallpaperCommand request, CancellationToken cancellationToken)
    {
        var kabeGamiCore = await _kabeGamiCoreRepository.GetKabeGamiCoreAsync(cancellationToken);
        if (kabeGamiCore.IsError)
        {
            return kabeGamiCore.Errors;
        }

        if (kabeGamiCore.Value.GalleryId == GalleryId.CreateEmpty())
        {
            return Errors.ChangeWallpaperCommandHandler.DefaultGalleryNotFound;
        }
        var gallery = await _galleryRepository.GetGalleryByIdAsync(kabeGamiCore.Value.GalleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var subGalleryId = SubGalleryId.Create(request.SubGalleryGuid);
        var subGallery = gallery.Value.GetSubGallery(subGalleryId);
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

        var wallpaperSystem = await _wallpaperSystemService.SetWallpaperSystemJob(imageResults, subGallery.Value.CronSchedule, cancellationToken);
        if (wallpaperSystem.IsError)
        {
            return wallpaperSystem.Errors;
        }

        return Unit.Value;
    }
}

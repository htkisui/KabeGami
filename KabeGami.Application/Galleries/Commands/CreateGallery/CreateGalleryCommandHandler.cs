using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Galleries.Results;
using KabeGami.Domain.Galleries;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.CreateGallery;
internal sealed class CreateGalleryCommandHandler(
    IGalleryRepository galleryRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
        : IRequestHandler<CreateGalleryCommand, ErrorOr<GalleryResult>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<GalleryResult>> Handle(CreateGalleryCommand request, CancellationToken cancellationToken)
    {
        var isGalleryNameAvailable = await _galleryRepository.IsGalleryNameAvailableAsync(request.Name, cancellationToken);
        if (isGalleryNameAvailable.IsError)
        {
            return isGalleryNameAvailable.Errors;
        }

        var gallery = Gallery.Create(request.Name);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        await _galleryRepository.AddGalleryAsync(gallery.Value, cancellationToken);

        var galleryResult = _mapper.Map<GalleryResult>(gallery.Value);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return galleryResult;
    }
}

using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Images.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Images.Queries.GetImages;
internal sealed class GetImagesQueryHandler(
    IImageRepository imageRepository,
    IMapper mapper)
        : IRequestHandler<GetImagesQuery, ErrorOr<List<ImageResult>>>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ErrorOr<List<ImageResult>>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
    {
        var images = await _imageRepository.GetImagesAsync(cancellationToken);

        var imageResults = images.Select(_mapper.Map<ImageResult>).ToList();

        return imageResults;
    }
}

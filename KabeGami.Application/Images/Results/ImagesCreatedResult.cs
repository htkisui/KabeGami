using ErrorOr;

namespace KabeGami.Application.Images.Results;
public sealed record ImagesCreatedResult(
    List<ImageResult> Images,
    List<Error> Errors);

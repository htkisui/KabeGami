namespace KabeGami.Application.Galleries.Results;
public sealed record GalleryResult(
    Guid GalleryGuid,
    string Name,
    List<Guid> ImageGuids);

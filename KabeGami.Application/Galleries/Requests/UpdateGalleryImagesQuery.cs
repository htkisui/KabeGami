namespace KabeGami.Application.Galleries.Requests;
public sealed record UpdateGalleryImagesQuery(
    Guid GalleryGuid,
    List<Guid> ImageGuids);

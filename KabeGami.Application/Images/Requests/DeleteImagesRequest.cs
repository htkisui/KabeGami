namespace KabeGami.Application.Images.Requests;
public sealed record DeleteImagesRequest(
    List<Guid> ImageGuids);

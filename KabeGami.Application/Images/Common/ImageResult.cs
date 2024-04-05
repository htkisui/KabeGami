namespace KabeGami.Application.Images.Common;
public record ImageResult(
    Guid Id,
    string Extension,
    string CategoryDirectoryPath,
    bool IsSFW);

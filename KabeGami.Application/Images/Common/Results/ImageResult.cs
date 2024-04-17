namespace KabeGami.Application.Images.Common.Results;
public sealed record ImageResult(
    Guid ImageGuidResult,
    string Extension,
    string CategoryDirectoryPath,
    bool IsSFW);

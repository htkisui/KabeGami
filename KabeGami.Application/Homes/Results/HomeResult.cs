namespace KabeGami.Application.Homes.Results;
public sealed record HomeResult(
    Guid HomeGuid,
    KabeResult? DefaultKabe,
    List<KabeResult> Kabes);

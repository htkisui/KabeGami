namespace KabeGami.Desktop.ViewModels.Common.Models;
public sealed class HomeDisplayModel(
    Guid homeGuid, 
    KabeDisplayModel defaultKabe, 
    List<KabeDisplayModel> kabes)
{
    public Guid HomeGuid { get; set; } = homeGuid;
    public KabeDisplayModel DefaultKabe { get; set; } = defaultKabe;
    public List<KabeDisplayModel> Kabes = kabes;
}

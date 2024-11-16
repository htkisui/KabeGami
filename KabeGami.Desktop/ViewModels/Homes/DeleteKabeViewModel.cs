using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Primitives;

namespace KabeGami.Desktop.ViewModels.Homes;
internal sealed class DeleteKabeViewModel(
    IHomeStore homeStore)
        : ViewModelBase
{
    private readonly IHomeStore _homeStore = homeStore;

    public async Task DeleteKabe(Guid kabeGuid)
    {
        await _homeStore.DeleteKabeAsync(kabeGuid);
    }
}

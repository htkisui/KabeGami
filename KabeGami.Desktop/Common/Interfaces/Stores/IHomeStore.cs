using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Interfaces.Stores;

public interface IHomeStore
{
    HomeDisplayModel Home { get; }
    Task CreateKabeAsync(string name, string combination, string cronSchedule, string galleryName);
    Task DeleteKabeAsync(Guid kabeGuid);
    Task SetDefaultKabeAsync(Guid kabeGuid);
    Task InitializeAsync();
}

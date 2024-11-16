using KabeGami.Desktop.Common.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace KabeGami.Desktop.ViewModels.Common.Primitives;
internal abstract class ViewModelBase 
    : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected readonly IErrorHandlingService _errorHandlingService;

    protected ViewModelBase()
    {
        _errorHandlingService = App.Container.GetRequiredService<IErrorHandlingService>();
    }

    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    

}

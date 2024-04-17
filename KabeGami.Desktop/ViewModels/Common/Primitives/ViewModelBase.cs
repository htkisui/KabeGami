using ErrorOr;
using System.ComponentModel;

namespace KabeGami.Desktop.ViewModels.Common.Primitives;
internal abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<(string, string)>? ErrorHandler;

    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    protected void Problem(List<Error> errors)
    {
        var title = String.Empty;
        var message = String.Empty;

        foreach (var error in errors)
        {
            title = error.Code;
            message += $"{error.Description}\n";
        }
        Problem(title, message);
    }

    protected void Problem(Error error)
    {
        var title = error.Code;
        var message = error.Description;
        Problem(title, message);
    }

    protected void Problem(string title, string message)
        => ErrorHandler?.Invoke(this, (title, message));
}

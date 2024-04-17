using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Windows.Input;

namespace KabeGami.Desktop.ViewModels.TopNav;
internal class TopNavViewModel : ViewModelBase
{
    private int x;
    public int X
    {
        get { return x; }
        set
        {
            x = value;
            OnPropertyChanged("X");
        }
    }

    public ICommand Test => new RelayCommand(async execute =>
    {
        X++;
        await Task.CompletedTask;
        Problem("aaa", "aaa");
    });
}

using KabeGami.Application.Images.Commands.CreateImage;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using MediatR;
using System.Windows.Input;

namespace KabeGami.Desktop.ViewModels.Test;
internal class TestViewModel(
    ISender sender) 
        : ViewModelBase
{
    private int count;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            OnPropertyChanged("Count");
        }
    }

    private readonly ISender _sender = sender;

    public ICommand ClickTest => new RelayCommand(async execute =>
    {
        Count++;
        await Task.CompletedTask;

    });


}

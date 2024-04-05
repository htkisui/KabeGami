using KabeGami.Application.Images.Commands.CreateImage;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using MediatR;
using System.Windows.Input;

namespace KabeGami.Desktop.ViewModels.Test;
internal class TestViewModel(ISender sender) : ViewModelBase
{
    private string message = string.Empty;
    public string Message
    {
        get { return message; }
        set
        {
            message = value;
            OnPropertyChanged("Message");
        }
    }

    private readonly ISender _sender = sender;

    public ICommand Test => new RelayCommand(async execute =>
    {
        var command = new CreateImageCommand(@"D:\Desktop\a.png", "PNG", "CgOriginal", true);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            Message = res.FirstError.ToString();
        }
        else
        {
            Message = "No Problem";
        }

    });
}

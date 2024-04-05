using KabeGami.Desktop.ViewModels.MainMenu;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace KabeGami.Desktop.Views.MainMenu;
/// <summary>
/// Interaction logic for MainMenuUserControl.xaml
/// </summary>
public partial class MainMenuUserControl : UserControl
{
    private readonly Forms.NotifyIcon _notifyIcon;
    private readonly MainMenuViewModel _viewModel;
    public MainMenuUserControl()
    {
        InitializeComponent();
        _notifyIcon = App.Container.GetRequiredService<Forms.NotifyIcon>();
        _viewModel = App.Container.GetRequiredService<MainMenuViewModel>();
        DataContext = _viewModel;
        InitializeNotifyIcon();
    }

    private void InitializeNotifyIcon()
    {
        try
        {
            _notifyIcon.Icon = new Icon(App.Configuration["MainMenuIconPath"]!);
            _notifyIcon.Visible = true;
        }
        catch (Exception ex)
        {
            throw new FileLoadException("MainMenuIconPath doesn't exist.", ex);
        }
    }

    public void Dispose()
    {
        _notifyIcon.Dispose();
    }
}

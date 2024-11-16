using KabeGami.Desktop.Common.Events.Views;
using KabeGami.Desktop.ViewModels;
using KabeGami.Desktop.Views.Homes;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;

namespace KabeGami.Desktop.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;
    private readonly Dictionary<string, string> routes = new()
    {
        {"Home", "KabeGami.Desktop.Views.Homes.HomeUserControl"},
        {"Image", "KabeGami.Desktop.Views.Images.ImageUserControl" },
        {"Gallery", "KabeGami.Desktop.Views.Galleries.GalleryUserControl" },
    };
    public MainWindow()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<MainWindowViewModel>();
        DataContext = _viewModel;
        MainContentControl.Content = App.Container.GetRequiredService<HomeUserControl>();
        _viewModel.Startup();
        Closing += OnClosing;
    }

    private void TopNav_MenuItemSelectedEvent(object sender, MenuItemSelectedEventArgs e)
    {
        var type = Type.GetType(routes[e.UserControlName]);
        if (type != null)
        {
            MainContentControl.Content = App.Container.GetRequiredService(type);
        }
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}
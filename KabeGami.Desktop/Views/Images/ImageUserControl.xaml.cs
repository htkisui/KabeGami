using KabeGami.Desktop.ViewModels.Images;
using KabeGami.Desktop.Views.Images.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Images;

/// <summary>
/// Interaction logic for ImageUserControl.xaml
/// </summary>
public partial class ImageUserControl : UserControl
{
    private readonly ImageViewModel _viewModel;
    public ImageUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<ImageViewModel>();
        DataContext = _viewModel;
        Loaded += (s,e) => _viewModel.InitializeImageViewer();
    }

    private void Button_CreateImageClick(object sender, RoutedEventArgs e)
    {
        CreateImageUserControl createImageUserControl = App.Container.GetRequiredService<CreateImageUserControl>();
        Window window = new Window
        {
            Content = createImageUserControl,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Title = "Create Images",
            Width = 1280,
            Height = 720,
            ResizeMode = ResizeMode.NoResize
        };

        window.ShowDialog();
    }

    private void Button_DeleteImageClick(object sender, RoutedEventArgs e)
    {
        DeleteImageUserControl deleteImageUserControl = App.Container.GetRequiredService<DeleteImageUserControl>();
        Window window = new Window
        {
            Content = deleteImageUserControl,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Title = "Delete Images",
            Width = 1280,
            Height = 720,
            ResizeMode = ResizeMode.NoResize
        };

        window.ShowDialog();
    }
}

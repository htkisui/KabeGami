using KabeGami.Desktop.ViewModels.Galleries;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Galleries.Common;
/// <summary>
/// Interaction logic for ManageGalleryUserControl.xaml
/// </summary>
public partial class ManageGalleryUserControl : UserControl
{
    private readonly ManageGalleryViewModel _viewModel;
    public ManageGalleryUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<ManageGalleryViewModel>();
        DataContext = _viewModel;
    }

    private void Button_UpdateGalleryImagesClick(object sender, RoutedEventArgs e)
    {
        UpdateGalleryImagesUserControl updateGalleryImagesUserControl = App.Container.GetRequiredService<UpdateGalleryImagesUserControl>();
        var button = sender as Button;
        var parameter = button?.Tag;
        if (parameter is Guid)
        {
            updateGalleryImagesUserControl.GalleryGuid = (Guid)parameter;
        }
        Window window = new Window
        {
            Content = updateGalleryImagesUserControl,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Title = "Update Gallery's Images",
            Width = 1280,
            Height = 720,
            ResizeMode = ResizeMode.NoResize
        };

        window.ShowDialog();
    }
}

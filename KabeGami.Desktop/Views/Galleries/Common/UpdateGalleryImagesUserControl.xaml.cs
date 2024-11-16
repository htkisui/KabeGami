using KabeGami.Desktop.ViewModels.Galleries;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Galleries.Common;
/// <summary>
/// Interaction logic for UpdateGalleryImagesUserControl.xaml
/// </summary>
public partial class UpdateGalleryImagesUserControl : UserControl
{
    private readonly UpdateGalleryImagesViewModel _viewModel;

    private const string onConfirmTitle = "Confirmation";
    private const string onConfirmMessage = "Update theses images ?";

    public Guid GalleryGuid
    {
        get => (Guid)GetValue(GalleryGuidProperty);
        set => SetValue(GalleryGuidProperty, value);
    }
    public static readonly DependencyProperty GalleryGuidProperty =
        DependencyProperty.Register(
            nameof(GalleryGuid),
            typeof(Guid),
            typeof(UpdateGalleryImagesUserControl),
            new PropertyMetadata(Guid.Empty));

    public UpdateGalleryImagesUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<UpdateGalleryImagesViewModel>();
        DataContext = _viewModel;
        Loaded += (s, e) => _viewModel.InitializeImageViewers(GalleryGuid);
    }

    private void Button_ValidateClick(object sender, RoutedEventArgs e)
    {
        var messageBox = MessageBox.Show(onConfirmMessage, onConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (messageBox == MessageBoxResult.Yes)
        {
            _ = _viewModel.UpdateGalleryImagesAsync();
            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}

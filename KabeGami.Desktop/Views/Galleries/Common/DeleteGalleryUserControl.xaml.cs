using KabeGami.Desktop.ViewModels.Galleries;
using KabeGami.Desktop.ViewModels.Homes;
using KabeGami.Desktop.Views.Homes.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Galleries.Common;
/// <summary>
/// Interaction logic for DeleteGalleryUserControl.xaml
/// </summary>
public partial class DeleteGalleryUserControl : UserControl
{
    private readonly DeleteGalleryViewModel _viewModel;
    public Guid GalleryGuid
    {
        get => (Guid)GetValue(GalleryGuidProperty);
        set => SetValue(GalleryGuidProperty, value);
    }
    public static readonly DependencyProperty GalleryGuidProperty =
        DependencyProperty.Register(
            nameof(GalleryGuid),
            typeof(Guid),
            typeof(DeleteGalleryUserControl),
            new PropertyMetadata(Guid.Empty));

    public DeleteGalleryUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<DeleteGalleryViewModel>();
    }

    private void Button_DeleteGalleryClick(object sender, RoutedEventArgs e)
    {
        _ = _viewModel.DeleteGallery(GalleryGuid);
    }
}

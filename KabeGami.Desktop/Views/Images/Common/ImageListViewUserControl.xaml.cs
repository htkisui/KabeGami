using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Images;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace KabeGami.Desktop.Views.Images.Common;

/// <summary>
/// Interaction logic for ImageListViewUserControl.xaml
/// </summary>
public partial class ImageListViewUserControl : UserControl
{
    private readonly ImageListViewViewModel _viewModel;

    public ObservableCollection<ImageDisplayModel> Images
    {
        get => (ObservableCollection<ImageDisplayModel>)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }
    public static readonly DependencyProperty ImagesProperty =
        DependencyProperty.Register(
            nameof(Images),
            typeof(ObservableCollection<ImageDisplayModel>),
            typeof(ImageListViewUserControl),
            new PropertyMetadata(new ObservableCollection<ImageDisplayModel>()));

    public Guid ImageViewerGuid
    {
        get => (Guid)GetValue(ImageViewerGuidProperty);
        set => SetValue(ImageViewerGuidProperty, value);
    }
    public static readonly DependencyProperty ImageViewerGuidProperty =
        DependencyProperty.Register(
            nameof(ImageViewerGuid),
            typeof(Guid),
            typeof(ImageListViewUserControl),
            new PropertyMetadata(Guid.Empty));

    public bool EnableRightClick
    {
        get => (bool)GetValue(EnableRightClickProperty);
        set => SetValue(EnableRightClickProperty, value);
    }
    public static readonly DependencyProperty EnableRightClickProperty = 
        DependencyProperty.Register(
            nameof(EnableRightClick),
            typeof(bool),
            typeof(ImageListViewUserControl),
            new PropertyMetadata(true));

    public ImageListViewUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<ImageListViewViewModel>();
    }

    private void ListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var selectedItem = imageListView.SelectedItem;
        if (selectedItem is ImageDisplayModel image)
        {
            _viewModel.ImageSelectedOnLeftClick(ImageViewerGuid, image);
        }

    }

    private void ListView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (EnableRightClick)
        {
            var selectedItem = imageListView.SelectedItem;
            if (selectedItem is ImageDisplayModel image)
            {
                Images.Remove(image);
                _viewModel.ImageSelectedOnRightClick(ImageViewerGuid, image);
            }
        }
    }
}

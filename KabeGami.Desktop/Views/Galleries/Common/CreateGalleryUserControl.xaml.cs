using KabeGami.Desktop.ViewModels.Galleries;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Galleries.Common;
/// <summary>
/// Interaction logic for CreateGalleryUserControl.xaml
/// </summary>
public partial class CreateGalleryUserControl : UserControl
{
    private readonly CreateGalleryViewModel _viewModel;
    public CreateGalleryUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<CreateGalleryViewModel>();
        DataContext = _viewModel;
    }
}

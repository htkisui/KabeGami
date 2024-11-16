using KabeGami.Desktop.ViewModels.Images;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Images.Common;
/// <summary>
/// Interaction logic for DeleteImageUserControl.xaml
/// </summary>
public partial class DeleteImageUserControl : UserControl
{
    private readonly DeleteImageViewModel _viewModel;

    private const string onConfirmTitle = "Confirmation";
    private const string onConfirmMessage = "Delete theses images ?";

    public DeleteImageUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<DeleteImageViewModel>();
        DataContext = _viewModel;
        Loaded += (s, e) => _viewModel.InitializeInputImageViewer();
    }

    private void Button_ValidateClick(object sender, RoutedEventArgs e)
    {
        var messageBox = MessageBox.Show(onConfirmMessage, onConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (messageBox == MessageBoxResult.Yes)
        {
            _ = _viewModel.DeleteImagesAsync();
            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}

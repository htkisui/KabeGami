using KabeGami.Desktop.ViewModels.Images;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;


namespace KabeGami.Desktop.Views.Images.Common;
/// <summary>
/// Interaction logic for CreateImageUserControl.xaml
/// </summary>
public partial class CreateImageUserControl : UserControl
{
    private readonly CreateImageViewModel _viewModel;

    private const string onConfirmTitle = "Confirmation";
    private const string onConfirmMessage = "Add theses images ?";

    public CreateImageUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<CreateImageViewModel>();
        DataContext = _viewModel;
    }

    private void Button_BrowseClick(object sender, RoutedEventArgs e)
    {
        var dialog = new Forms.FolderBrowserDialog();
        dialog.ShowNewFolderButton = true;
        dialog.RootFolder = Environment.SpecialFolder.Desktop;

        Forms.DialogResult result = dialog.ShowDialog();

        if (result == Forms.DialogResult.OK)
        {
            _ = _viewModel.GetImagesPathFromDirectoryPathAsync(dialog.SelectedPath);
        }
    }

    private void Button_ValidateClick(object sender, RoutedEventArgs e)
    {
        var messageBox = MessageBox.Show(onConfirmMessage, onConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (messageBox == MessageBoxResult.Yes)
        {
            _ = _viewModel.CreateImagesAsync();
            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}

using KabeGami.Desktop.ViewModels.Homes;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Homes.Common;
/// <summary>
/// Interaction logic for CreateKabeUserControl.xaml
/// </summary>
public partial class CreateKabeUserControl : UserControl
{
    private readonly CreateKabeViewModel _viewModel;
    public CreateKabeUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<CreateKabeViewModel>();
        DataContext = _viewModel;
    }
}

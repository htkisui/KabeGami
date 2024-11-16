using KabeGami.Desktop.ViewModels.Homes;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Homes.Common;
/// <summary>
/// Interaction logic for KabeUserControl.xaml
/// </summary>
public partial class ManageKabeUserControl : UserControl
{
    private readonly ManageKabeViewModel _viewModel;
    public ManageKabeUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<ManageKabeViewModel>();
        DataContext = _viewModel;
    }
}

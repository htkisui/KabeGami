using KabeGami.Desktop.ViewModels.Test;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Test;
/// <summary>
/// Interaction logic for TestUserControl.xaml
/// </summary>
public partial class TestUserControl : UserControl
{
    private readonly TestViewModel _viewModel;

    public TestUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<TestViewModel>();
        DataContext = _viewModel;
    }
}

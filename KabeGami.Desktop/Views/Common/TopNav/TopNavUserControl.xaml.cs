using KabeGami.Desktop.ViewModels.TopNav;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Common.TopNav;
/// <summary>
/// Interaction logic for TopNavUserControl.xaml
/// </summary>
public partial class TopNavUserControl : UserControl
{
    public TopNavUserControl()
    {
        InitializeComponent();
        DataContext = new TopNavViewModel();
    }
}

using KabeGami.Desktop.Common.Events.Views;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Common.Layouts;
/// <summary>
/// Interaction logic for TopNavUserControl.xaml
/// </summary>
public sealed partial class TopNavUserControl : UserControl
{
    public event EventHandler<MenuItemSelectedEventArgs>? MenuItemSelectedEvent;

    public TopNavUserControl()
    {
        InitializeComponent();
    }

    private void MenuItemSelected_Click(object sender, RoutedEventArgs e)
    {
        MenuItem menuItem = (MenuItem)sender;
        MenuItemSelectedEvent?.Invoke(this, new(menuItem.Header.ToString()!));
    }
}

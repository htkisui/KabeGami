using KabeGami.Desktop.ViewModels.Homes;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Homes.Common;
/// <summary>
/// Interaction logic for DeleteKabeUserControl.xaml
/// </summary>
public partial class DeleteKabeUserControl : UserControl
{
    private readonly DeleteKabeViewModel _viewModel;

    public Guid KabeGuid
    {
        get => (Guid)GetValue(KabeGuidProperty);
        set => SetValue(KabeGuidProperty, value);
    }
    public static readonly DependencyProperty KabeGuidProperty =
        DependencyProperty.Register(
            nameof(KabeGuid),
            typeof(Guid),
            typeof(DeleteKabeUserControl),
            new PropertyMetadata(Guid.Empty));

    public DeleteKabeUserControl()
    {
        InitializeComponent();
        _viewModel = App.Container.GetRequiredService<DeleteKabeViewModel>();
    }

    private void DeleteKabe_Click(object sender, RoutedEventArgs e)
    {
        _ = _viewModel.DeleteKabe(KabeGuid);
    }
}

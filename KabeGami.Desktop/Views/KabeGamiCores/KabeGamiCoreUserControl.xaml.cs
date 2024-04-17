using Gma.System.MouseKeyHook;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using KabeGami.Desktop.ViewModels.KabeGamiCores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.KabeGamiCores;
/// <summary>
/// Interaction logic for MainMenuUserControl.xaml
/// </summary>
public partial class KabeGamiCoreUserControl : UserControl
{
    private readonly Dictionary<Combination, Action> _assignment = [];
    private readonly KabeGamiCoreViewModel _viewModel;

    public KabeGamiCoreUserControl()
    {
        _viewModel = App.Container.GetRequiredService<KabeGamiCoreViewModel>();
        DataContext = _viewModel;
        InitializeComponent();
        InitializeEventHandling();
        InitializeGlobalEvents();
    }

    private void InitializeEventHandling()
        => _viewModel.ErrorHandler += ProblemDisplay;

    private void InitializeGlobalEvents()
    {
        var kabeGamiGlobalEvents = App.Container.GetRequiredService<IOptions<KabeGamiGlobalEvents>>();
        var assignment = new Dictionary<Combination, Action>();

        foreach (var combinationString in kabeGamiGlobalEvents.Value.Combinations)
        {
            var combination = Combination.FromString(combinationString);
            assignment[combination] = async () => await _viewModel.ChangeWallpaper(combinationString);
        }

        Hook.GlobalEvents().OnCombination(assignment);
    }

    private void ProblemDisplay(object? sender, (string, string) error)
        => MessageBox.Show(error.Item2, error.Item1, MessageBoxButton.OK, MessageBoxImage.Error);
}

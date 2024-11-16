using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Common.Widgets;

/// <summary>
/// Interaction logic for ProgressBarUserControl.xaml
/// </summary>
public partial class ProgressBarUserControl : UserControl
{
    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(int),
            typeof(ProgressBarUserControl),
            new PropertyMetadata(0));

    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(
            nameof(Maximum),
            typeof(int),
            typeof(ProgressBarUserControl),
            new PropertyMetadata(0));

    public ProgressBarUserControl()
    {
        InitializeComponent();
    }
}

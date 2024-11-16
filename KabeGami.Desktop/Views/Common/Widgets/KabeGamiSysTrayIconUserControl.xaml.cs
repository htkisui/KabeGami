using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace KabeGami.Desktop.Views.Common.Widgets;
/// <summary>
/// Interaction logic for HomeMenuUserControl.xaml
/// </summary>
public sealed partial class KabeGamiSysTrayIconUserControl : UserControl
{
    private Forms.NotifyIcon _notifyIcon = null!;

    private const string exitItemName = "Exit";
    private const string initializeNotifyIconFailedMessage = "MainMenuIconPath doesn't exist.";
    private const string mainMenuIconPathKey = "MainMenuIconPath";
    private const string mainWindowItemName = "Main";
    private const string onExitTitle = "Confirmation";
    private const string onExitMessage = "Close ?";


    public KabeGamiSysTrayIconUserControl()
    {
        InitializeComponent();
        InitializeNotifyIcon();
    }
    private void InitializeNotifyIcon()
    {
        try
        {
            _notifyIcon = App.Container.GetRequiredService<Forms.NotifyIcon>();
            _notifyIcon.Icon = new Icon(App.Configuration[mainMenuIconPathKey]!);
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add(mainWindowItemName, null, OnMainWindow);
            _notifyIcon.ContextMenuStrip.Items.Add(exitItemName, null, OnExit);
            _notifyIcon.DoubleClick += OnMainWindow;
        }
        catch (Exception ex)
        {
            throw new FileLoadException(initializeNotifyIconFailedMessage, ex);
        }
    }

    private void OnExit(object? sender, EventArgs e)
    {
        var result = MessageBox.Show(onExitMessage, onExitTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
    private void OnMainWindow(object? sender, EventArgs e)
    {
        var mainWindow = App.Container.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    public void Dispose()
        => _notifyIcon.Dispose();
}

namespace KabeGami.Desktop.Common.Events.Views;
public sealed class MenuItemSelectedEventArgs(
    string userControlName) : EventArgs
{
    public string UserControlName { get; } = userControlName;
}

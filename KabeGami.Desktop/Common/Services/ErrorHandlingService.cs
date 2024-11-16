using ErrorOr;
using KabeGami.Desktop.Common.Interfaces.Services;
using System.Windows;

namespace KabeGami.Desktop.Common.Services;
internal sealed class ErrorHandlingService
    : IErrorHandlingService
{
    public void HandlerErrors(List<Error> errors)
    {
        string errorMessage = string.Join(Environment.NewLine, errors.Select(e => $"{e.Code} : {e.Description}").ToList());
        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

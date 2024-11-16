
using ErrorOr;

namespace KabeGami.Desktop.Common.Interfaces.Services;
public interface IErrorHandlingService
{
    void HandlerErrors(List<Error> errors);
}

using ErrorOr;
using KabeGami.Application.Homes.Results;
using MediatR;

namespace KabeGami.Application.Homes.Queries.GetKabes;
public sealed record GetKabesQuery
    : IRequest<ErrorOr<List<KabeResult>>>;

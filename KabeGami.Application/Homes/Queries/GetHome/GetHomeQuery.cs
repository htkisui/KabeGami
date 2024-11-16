using ErrorOr;
using KabeGami.Application.Homes.Results;
using MediatR;

namespace KabeGami.Application.Homes.Queries.GetHome;
public sealed record class GetHomeQuery
    : IRequest<ErrorOr<HomeResult>>;

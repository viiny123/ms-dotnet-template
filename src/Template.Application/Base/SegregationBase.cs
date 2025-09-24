using MediatR;
using Template.Domain.Base;

namespace Template.Application.Base;

public abstract class SegregationBase<TRequest> : Paginate<Result>, IRequest<Result>
    where TRequest : SegregationBase<TRequest>
{
}

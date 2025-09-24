namespace Template.Application.Base;

public abstract class QueryBase<TRequest> : SegregationBase<TRequest>
    where TRequest : SegregationBase<TRequest>
{
}

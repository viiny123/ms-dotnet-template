using Microsoft.AspNetCore.Http;
using Template.Application.Base;
using Template.Application.Base.Error;
using Template.Domain.AggregatesModel.ValueAggreate;

namespace Template.Application.Value.V1.Queries.GetById;

public class GetValueByIdQueryHandler : HandlerBase<GetValueByIdQuery>
{
    private readonly IValueRepository _valueRepository;

    public GetValueByIdQueryHandler(IValueRepository valueRepository)
    {
        _valueRepository = valueRepository;
    }

    public override async Task<Result> Handle(GetValueByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _valueRepository.GetValueByIdAsync(request.Id);

        if (value is null)
        {
            Result.AddError(ErrorCatalog.Value.GetByIdNotFound, StatusCodes.Status404NotFound);

            return Result;
        }

        Result.Data = value;

        return Result;
    }
}

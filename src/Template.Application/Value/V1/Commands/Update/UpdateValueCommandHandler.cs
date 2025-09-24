using Microsoft.AspNetCore.Http;
using Template.Application.Base;
using Template.Application.Base.Error;
using Template.Domain.AggregatesModel.ValueAggreate;
using Template.Domain.Base;

namespace Template.Application.Value.V1.Commands.Update;

public class UpdateValueCommandHandler : HandlerBase<UpdateValueCommand>
{

    private readonly IValueRepository _valueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateValueCommandHandler(IValueRepository valueRepository,
        IUnitOfWork unitOfWork)
    {
        _valueRepository = valueRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(UpdateValueCommand request, CancellationToken cancellationToken)
    {

        var value = await _valueRepository.GetValueByIdAsync(request.Id);

        if (value is null)
        {
            Result.AddError(ErrorCatalog.Value.GetByIdNotFound, StatusCodes.Status404NotFound);

            return Result;
        }

        SetValuesToUpdate(value, request);
        await _valueRepository.UpdateAsync(value);
        await _unitOfWork.SaveAsync();

        return Result;
    }

    private static void SetValuesToUpdate(Domain.AggregatesModel.ValueAggreate.Value value, UpdateValueCommand request)
    {
        value.Description = request.Description;
    }
}

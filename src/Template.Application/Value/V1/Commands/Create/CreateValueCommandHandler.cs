using Microsoft.AspNetCore.Http;
using Template.Application.Base;
using Template.Application.Base.Error;
using Template.Domain.AggregatesModel.ValueAggreate;
using Template.Domain.Base;

namespace Template.Application.Value.V1.Commands.Create;

public class CreateValueCommandHandler : HandlerBase<CreateValueCommand>
{

    private readonly IValueRepository _valueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateValueCommandHandler(IValueRepository valueRepository,
        IUnitOfWork unitOfWork)
    {
        _valueRepository = valueRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(CreateValueCommand request, CancellationToken cancellationToken)
    {
        //Simulate bussiness error
        if (request.Code == "-1")
        {
            Result.AddError(ErrorCatalog.Value.CodeCanBeNegativeNumber, StatusCodes.Status422UnprocessableEntity);
            return Result;
        }

        var value = CreateValue(request);
        await _valueRepository.AddAsync(value);
        await _unitOfWork.SaveAsync();

        Result.Data = new CreateCommandValueResponse(value.Id);

        return Result;
    }

    private static Domain.AggregatesModel.ValueAggreate.Value CreateValue(CreateValueCommand request)
    {
        return new Domain.AggregatesModel.ValueAggreate.Value()
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Description = request.Description,
            Status = Status.Active,
            Timestamp = DateTime.Now
        };
    }
}

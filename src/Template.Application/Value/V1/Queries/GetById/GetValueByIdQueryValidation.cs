using FluentValidation;
using Template.Application.Base.Error;
using Template.Application.Base.Extension;

namespace Template.Application.Value.V1.Queries.GetById;

public class GetValueByIdQueryValidation : AbstractValidator<GetValueByIdQuery>
{
    public GetValueByIdQueryValidation()
    {
        RuleFor(r => r)
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.BaseInvalidRequest);

        RuleFor(r => r.Id)
            .NotEmpty()
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.GetByIdNotFound);
    }
}

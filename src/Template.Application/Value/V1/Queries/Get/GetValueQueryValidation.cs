using FluentValidation;
using Template.Application.Base.Error;
using Template.Application.Base.Extension;

namespace Template.Application.Value.V1.Queries.Get;

public class GetValueQueryValidation : AbstractValidator<GetValueQuery>
{
    public GetValueQueryValidation()
    {
        RuleFor(r => r)
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.BaseInvalidRequest);
    }
}

using FluentValidation;
using Template.Application.Base.Error;
using Template.Application.Base.Extension;

namespace Template.Application.Value.V1.Commands.Update;

public class UpdateCommandValidation : AbstractValidator<UpdateValueCommand>
{
    public UpdateCommandValidation()
    {
        RuleFor(r => r)
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.BaseInvalidRequest);
    }
}

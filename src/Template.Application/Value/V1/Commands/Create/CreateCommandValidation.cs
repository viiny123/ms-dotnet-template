using FluentValidation;
using Template.Application.Base.Error;
using Template.Application.Base.Extension;
using Template.Domain.AggregatesModel.ValueAggreate;

namespace Template.Application.Value.V1.Commands.Create;

public class CreateCommandValidation : AbstractValidator<CreateValueCommand>
{
    public CreateCommandValidation(IValueRepository valueRepository)
    {
        RuleFor(r => r)
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.BaseInvalidRequest);

        RuleFor(r => r.Code)
            .NotEmpty()
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.CreateCodeIsNullOrEmpty)
            .MustAsync(async (code, ct) => !await valueRepository.IsCodeDuplicate(code))
            .WithErrorCatalog(ErrorCatalog.Value.CreateCodeIsDuplicated);

        RuleFor(r => r.Description)
            .NotEmpty()
            .NotNull()
            .WithErrorCatalog(ErrorCatalog.Value.CreateDescriptionIsNullOrEmpty);

        RuleFor(r => r)
            .SetValidator(new CreateCommandValidation2())
            .When(r => !string.IsNullOrEmpty(r.Description));
    }
}

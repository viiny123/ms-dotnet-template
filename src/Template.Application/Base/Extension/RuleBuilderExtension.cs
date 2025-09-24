using FluentValidation;
using Template.Application.Base.Error;

namespace Template.Application.Base.Extension;

public static class RuleBuilderOptionExtension
{
    public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry)
    {
        return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage(errorCatalogEntry.Message);
    }
}

using Template.Application.Base.Error;

namespace Template.Application.Base;

public class Result
{
    private readonly List<ErrorDetail> _errors;
    public IReadOnlyCollection<ErrorDetail> Errors => _errors;
    public int? StatusCode { get; set; }
    public object Data { get; set; }
    public bool IsValid => Errors.Any() == false;

    public Result()
    {
        _errors = new List<ErrorDetail>();
    }

    public void AddError(ErrorCatalogEntry errorCatalogEntry, int statusCode)
    {
        StatusCode = statusCode;
        _errors.Add(new ErrorDetail(errorCatalogEntry.Code, errorCatalogEntry.Message));
    }

    public void AddErrors(IEnumerable<ErrorDetail> errorDetails, int statusCode, string property = null)
    {
        StatusCode = statusCode;
        _errors.AddRange(errorDetails);
    }

    public void AddValidationErrors(IEnumerable<ErrorCatalogEntry> errorCatalogEntry, int statusCode)
    {
        StatusCode = statusCode;
        _errors.AddRange(errorCatalogEntry.Select(x => new ErrorDetail(x.Code, x.Message, x.Property)));
    }
}

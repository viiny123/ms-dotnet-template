namespace Template.Application.Base.Error;

public class ErrorDetail
{
    public ErrorDetail(string errorCode, string message, string property = null)
    {
        Property = property;
        ErrorCode = errorCode;
        Message = message;
    }

    public ErrorDetail(ErrorCatalogEntry errorCatalogEntry)
    {
        ErrorCode = errorCatalogEntry.Code;
        Message = errorCatalogEntry.Message;
    }

    public string Property { get; set; }
    public string ErrorCode { get; set; }
    public string Message { get; set; }
}

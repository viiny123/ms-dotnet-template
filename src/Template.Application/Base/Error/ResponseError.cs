namespace Template.Application.Base.Error;

public class ResponseError<TNotifications>
{
    public ErrorCode? Code { get; }
    public string Message { get; }
    public IEnumerable<TNotifications> Errors { get; }


    public ResponseError()
    {
    }

    public ResponseError(ErrorCode? code,
        IEnumerable<TNotifications> errors,
        string message)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }
}

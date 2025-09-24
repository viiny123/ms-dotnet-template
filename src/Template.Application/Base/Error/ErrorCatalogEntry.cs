namespace Template.Application.Base.Error;

public readonly struct ErrorCatalogEntry
{
    public ErrorCatalogEntry(string code, string message, string property = null)
    {
        Property = property;
        Code = code;
        Message = message;
    }

    public string Property { get; }
    public string Code { get; }
    public string Message { get; }

    public static implicit operator ErrorCatalogEntry((string Code, string Message) codeMessage) =>
        new(codeMessage.Code, codeMessage.Message);
}

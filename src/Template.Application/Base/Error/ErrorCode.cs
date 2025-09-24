using System.ComponentModel;

namespace Template.Application.Base.Error;

public enum ErrorCode
{
    [Description("Malformed request syntax")]
    BAD_REQUEST = 0,

    [Description("Invalid user to use the resource")]
    UNAUTHORIZED = 1,

    [Description("The user is authorized to use the service, " +
        "but not the requested resource")]
    FORBIDDEN = 2,

    [Description("Requested resource could not be found or does not exist")]
    NOT_FOUND = 3,

    [Description("There is a resource with the same characteristics sent")]
    CONFLICT = 4,

    [Description("Validation of business rules executed")]
    UNPROCESSABLE_ENTITY = 5,

    [Description("Unexpected error")]
    INTERNAL_SERVER_ERROR = 6,

    [Description("Integration service temporarily out of service")]
    SERVICE_UNAVAILABLE = 7,

    [Description("Integration Validation rule executed")]
    PRECONDITION_FAILED = 8
}

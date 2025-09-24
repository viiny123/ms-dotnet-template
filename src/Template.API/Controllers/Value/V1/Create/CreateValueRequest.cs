using Template.Application.Value.V1.Commands.Create;

namespace Template.API.Controllers.Value.V1.Create;

public class CreateValueRequest
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string NewProperty { get; set; }


    public static implicit operator CreateValueCommand(CreateValueRequest request)
    {
        if (request is null)
        {
            return null;
        }

        return new CreateValueCommand(request.Code,
            request.Description);
    }

}

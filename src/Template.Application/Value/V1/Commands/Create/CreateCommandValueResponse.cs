namespace Template.Application.Value.V1.Commands.Create;

public class CreateCommandValueResponse
{
    public Guid Id { get; set; }

    public CreateCommandValueResponse(Guid id)
    {
        Id = id;
    }
}

using Template.Domain.AggregatesModel.ValueAggreate;

namespace Template.Application.Value.V1.Queries.Get;

public class GetValueQueryResponse
{
    public GetValueQueryResponse(Guid id,
        string code,
        string description,
        Status status,
        DateTime timestamp)
    {
        Id = id;
        Code = code;
        Description = description;
        Status = status;
        Timestamp = timestamp;
    }

    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime Timestamp { get; set; }
}

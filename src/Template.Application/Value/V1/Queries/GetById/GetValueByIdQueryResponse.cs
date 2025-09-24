using Template.Domain.AggregatesModel.ValueAggreate;

namespace Template.Application.Value.V1.Queries.GetById;

public class GetValueByIdQueryResponse
{
    public GetValueByIdQueryResponse(Guid id,
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


    public static implicit operator GetValueByIdQueryResponse(Domain.AggregatesModel.ValueAggreate.Value value)
    {
        if (value is null)
        {
            return null;
        }

        return new GetValueByIdQueryResponse(value.Id,
            value.Code,
            value.Description,
            value.Status,
            value.Timestamp);
    }
}

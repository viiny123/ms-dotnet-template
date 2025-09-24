using Template.Application.Base;

namespace Template.Application.Value.V1.Queries.GetById;

public class GetValueByIdQuery : QueryBase<GetValueByIdQuery>
{
    public Guid Id { get; set; }

    public GetValueByIdQuery(Guid id)
    {
        Id = id;
    }
}

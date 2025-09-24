using Template.Application.Base;

namespace Template.Application.Value.V1.Queries.Get;

public class GetValueQuery : QueryBase<GetValueQuery>
{
    public string Code { get; set; }
    public string Description { get; set; }

    public GetValueQuery(
        string code,
        string description)
    {
        Code = code;
        Description = description;
    }

    public GetValueQuery WithPaginated(int pageSize, int pageNumber)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;

        return this;
    }
}

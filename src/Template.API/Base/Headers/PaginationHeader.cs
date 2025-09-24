using Microsoft.AspNetCore.Mvc;

namespace Template.API.Base.Headers;

public class PaginationHeader
{
    /// <summary>
    /// Page number
    /// </summary>
    [FromHeader(Name = "x-page-number")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    [FromHeader(Name = "x-page-size")]
    public int PageSize { get; set; }

    public PaginationHeader()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationHeader(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize;
    }
}

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.API.Base.Presenters;
using Template.API.Extensions;
using Template.Application.Value.V1.Queries.Get;
using Template.Domain.Base;

namespace Template.API.Controllers.Value.V1;

public partial class ValueController
{
    /// <summary>
    /// Get values
    /// </summary>
    /// <param name="code">Code</param>
    /// <param name="description">Description</param>
    /// <param name="emitError">EmitError</param>
    /// <param name="pageNumber"> Number of page</param>
    /// <param name="pageSize"> Max 10</param>
    /// <returns></returns>
    /// <response code="200">List of values</response>
    /// <response code="500">Internal Server Errror</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetValueQueryResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetValuesV1Async([FromQuery] string code,
        [FromQuery] string description,
        [FromQuery] bool emitError,
        [FromHeader(Name = "x-page-number")] int pageNumber = 1,
        [FromHeader(Name = "x-page-size")] int pageSize = 10)
    {
        if (emitError)
        {
#pragma warning disable CA2201 // Do not raise reserved exception types
            throw new System.Exception("Standard error response");
#pragma warning restore CA2201 // Do not raise reserved exception types
        }

        var queryRequest = new GetValueQuery(code, description)
            .WithPaginated(pageSize, pageNumber);
        var result = await _mediator.Send(queryRequest);

        var dataPaginate = (Paginate<GetValueQueryResponse>)result.Data;

        Response.Headers.AddPaginationData(dataPaginate);

        var response = ApiResponseFactory.Cast(dataPaginate.PageData, HttpStatusCode.OK);

        return response;
    }
}

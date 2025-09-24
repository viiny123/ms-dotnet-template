using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.API.Base.Presenters;
using Template.Application.Base;
using Template.Application.Base.Error;
using Template.Application.Value.V1.Queries.GetById;

namespace Template.API.Controllers.Value.V1;

public partial class ValueController
{
    /// <summary>
    /// Get value
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    /// <response code="200">Value</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Errror</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetValueByIdQueryResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetValueByIdV1Async([FromRoute] Guid id)
    {
        var queryRequest = new GetValueByIdQuery(id);
        var result = await _mediator.Send(queryRequest);
        var response = ApiResponseFactory.Cast((Result)result, HttpStatusCode.OK);

        return response;
    }
}

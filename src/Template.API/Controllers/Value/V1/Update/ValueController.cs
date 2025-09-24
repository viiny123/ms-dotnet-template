using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.API.Base.Presenters;
using Template.API.Controllers.Value.V1.Update;
using Template.Application.Base;
using Template.Application.Value.V1.Commands.Create;
using Template.Application.Value.V1.Commands.Update;

namespace Template.API.Controllers.Value.V1;

public partial class ValueController
{
    /// <summary>
    /// Update value
    /// </summary>
    /// <param name="id">Unique identifier of Value</param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="204">Value update</response>
    /// <response code="400">Bad Request</response>
    /// <response code="422">Bussiness rules violated</response>
    /// <response code="500">Internal Server Errror</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CreateCommandValueResponse), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateValueV1Async([FromRoute] Guid id,
        [FromBody] UpdateValueRequest request)
    {
        var command = new UpdateValueCommand(id, request.Description);
        var result = await _mediator.Send(command);
        var response = ApiResponseFactory.Cast((Result)result, HttpStatusCode.NoContent);

        return response;
    }
}

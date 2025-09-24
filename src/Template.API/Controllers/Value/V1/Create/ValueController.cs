using System.Net;
using Microsoft.AspNetCore.Mvc;
using Template.API.Base.Presenters;
using Template.API.Controllers.Value.V1.Create;
using Template.Application.Base;
using Template.Application.Base.Error;
using Template.Application.Value.V1.Commands.Create;
using Template.Application.Value.V1.Events.SaveStatus;

namespace Template.API.Controllers.Value.V1;

public partial class ValueController
{
    /// <summary>
    /// Create value
    /// </summary>
    /// <returns></returns>
    /// <response code="201">Value created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="422">Bussiness rules violated</response>
    /// <response code="500">Internal Server Errror</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateCommandValueResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateValueV1Async([FromBody] CreateValueRequest request)
    {
        var statusEvent = new SaveStatusEvent(request.Code, DateTime.Now);
        _ = _mediator.Publish(statusEvent);

        CreateValueCommand command = request;
        var result = await _mediator.Send(command);
        var response = ApiResponseFactory.Cast(result, HttpStatusCode.Created);

        return response;
    }
}

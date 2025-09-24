using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Template.API.Controllers.Value.V1;

[ExcludeFromCodeCoverage]
[ApiConventionType(typeof(DefaultApiConventions))]
[ApiController]
[ProducesErrorResponseType(typeof(void))]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public partial class ValueController : ControllerBase
{
    private readonly IMediator _mediator;

    public ValueController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
}

using Microsoft.Extensions.Logging;
using Template.Application.Base;

namespace Template.Application.Value.V1.Events.SaveStatus;

public class SaveStatusHandler : EventHandlerBase<SaveStatusEvent>
{

    private readonly ILogger<SaveStatusHandler> _logger;

    public SaveStatusHandler(ILogger<SaveStatusHandler> logger)
    {
        _logger = logger;
    }

    public override async Task Handle(SaveStatusEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Status event received");

        await Task.Run(() => Thread.Sleep(1000), cancellationToken);

        _logger.LogInformation("Status event saved");
    }
}

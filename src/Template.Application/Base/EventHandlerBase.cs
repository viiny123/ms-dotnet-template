using MediatR;

namespace Template.Application.Base;

public abstract class EventHandlerBase<TNotification> : INotificationHandler<TNotification>
    where TNotification : EventBase
{
    public abstract Task Handle(TNotification notification, CancellationToken cancellationToken);
}

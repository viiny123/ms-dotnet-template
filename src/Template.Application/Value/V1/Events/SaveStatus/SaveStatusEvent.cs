using Template.Application.Base;

namespace Template.Application.Value.V1.Events.SaveStatus;

public class SaveStatusEvent : EventBase
{
    public SaveStatusEvent(string code,
        DateTime timestamp)
    {
        Code = code;
        Timestamp = timestamp;
    }

    public string Code { get; set; }
    public DateTime Timestamp { get; set; }
}

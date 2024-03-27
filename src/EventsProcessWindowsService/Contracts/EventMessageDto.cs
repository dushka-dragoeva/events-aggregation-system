namespace EventsProcessWindowsService.Contracts
{
    public class EventMessageDto
    {
        public EventType Type { get; set; }

        public object Data { get; set; }
    }
}
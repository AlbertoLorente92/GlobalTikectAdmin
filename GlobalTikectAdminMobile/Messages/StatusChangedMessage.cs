using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Messages
{
    public class StatusChangedMessage
    {
        public Guid EventId { get; }
        public EventStatusEnum Status { get; }
        public StatusChangedMessage(Guid eventId, EventStatusEnum status)
        {
            EventId = eventId;
            Status = status;
        }
    }
}

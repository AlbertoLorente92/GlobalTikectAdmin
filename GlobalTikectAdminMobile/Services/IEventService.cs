using GlobalTikectAdminMobile.Models;

namespace GlobalTikectAdminMobile.Services
{
    public interface IEventService
    {
        Task<List<EventModel>> GetEventsAsync();
        Task<EventModel?> GetEventAsync(Guid id);
        Task<bool> UpdateStatus(Guid id, EventStatusModel status);
    }
}

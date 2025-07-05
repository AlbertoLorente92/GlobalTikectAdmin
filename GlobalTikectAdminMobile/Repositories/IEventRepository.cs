using GlobalTikectAdminMobile.Models;

namespace GlobalTikectAdminMobile.Repositories
{
    public interface IEventRepository
    {
        Task<List<EventModel>> GetEventsAsync();
        Task<EventModel?> GetEventAsync(Guid id);
        Task<bool> UpdateStatus(Guid id, EventStatusModel status);
    }
}

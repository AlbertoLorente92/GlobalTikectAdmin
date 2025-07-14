using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Repositories;

namespace GlobalTikectAdminMobile.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Task<bool> CreateEvent(EventModel model)
            => _eventRepository.CreateEvent(model);

        public Task<bool> DeleteEvent(Guid id)
         => _eventRepository.DeleteEvent(id);

        public Task<bool> EditEvent(EventModel model)
            => _eventRepository.EditEvent(model);

        public Task<EventModel?> GetEventAsync(Guid id) 
            => _eventRepository.GetEventAsync(id);

        public Task<List<EventModel>> GetEventsAsync() 
            => _eventRepository.GetEventsAsync();

        public Task<bool> UpdateStatus(Guid id, EventStatusModel status) 
            => _eventRepository.UpdateStatus(id, status);
    }
}

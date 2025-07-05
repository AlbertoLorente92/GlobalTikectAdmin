using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Services;
using System.Collections.ObjectModel;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class EventListOverviewViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<EventListItemViewModel> _events = new();

        [ObservableProperty]
        private EventListItemViewModel? _selectedEvent;

        private readonly IEventService _eventService;

        public EventListOverviewViewModel(IEventService eventService)
        {
            _eventService = eventService;
            GetEvents();
        }

        private async void GetEvents()
        {
            List<EventModel> events = await _eventService.GetEventsAsync();
            List<EventListItemViewModel> listItems = new();
            foreach (var @event in events)
            {
                listItems.Add(MapEventModelToEventListItemViewModel(@event));
            }

            Events.Clear();
            Events = listItems.ToObservableCollection();
        }

        private static EventListItemViewModel MapEventModelToEventListItemViewModel(EventModel @event)
        {
            var category = new CategoryViewModel
            {
                Id = @event.Category.Id,
                Name = @event.Category.Name,
            };

            return new EventListItemViewModel(
                @event.Id,
                @event.Name,
                @event.Price,
                @event.Date,
                @event.Artists,
                (EventStatusEnum)@event.Status,
                @event.ImageUrl,
                category);
        }
    }
}

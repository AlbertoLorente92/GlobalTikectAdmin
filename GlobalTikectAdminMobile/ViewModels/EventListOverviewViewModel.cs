using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GlobalTikectAdminMobile.Messages;
using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Services;
using GlobalTikectAdminMobile.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class EventListOverviewViewModel : ViewModelBase, IRecipient<EventAddedOrChangedMessage>, IRecipient<EventDeletedMessage>
    {
        [ObservableProperty]
        private ObservableCollection<EventListItemViewModel> _events = new();

        [ObservableProperty]
        private EventListItemViewModel? _selectedEvent;

        private readonly IEventService _eventService;
        private readonly INavigationService _navigationService;

        public EventListOverviewViewModel(IEventService eventService, INavigationService navigationService)
        {
            _eventService = eventService;
            _navigationService = navigationService;

            WeakReferenceMessenger.Default.Register<EventAddedOrChangedMessage>(this);
            WeakReferenceMessenger.Default.Register<EventDeletedMessage>(this);
        }

        [RelayCommand]
        private async Task NavigateToSelectedDetail()
        {
            if (SelectedEvent is not null)
            {
                await _navigationService.GoToEventDetail(SelectedEvent.Id);
                SelectedEvent = null;
            }
        }

        [RelayCommand]
        private async Task NavigateToAddEvent()
        {
            await _navigationService.GoToAddEvent();
        }

        public override async Task LoadAsync()
        {
            if(Events.Count == 0)
            {
                await Loading(GetEvents);
            }
        }

        private async Task GetEvents()
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

        public async Task Receive(EventAddedOrChangedMessage message)
        {
            Events.Clear();
            await GetEvents();
        }

        void IRecipient<EventAddedOrChangedMessage>.Receive(EventAddedOrChangedMessage message)
        {
            Events.Clear();
            Task.Run(async () => await GetEvents());
        }

        public void Receive(EventDeletedMessage message)
        {
            var deletedEvent = Events.FirstOrDefault(e => e.Id == message.EventId);
            if (deletedEvent is not null)
            {
                Events.Remove(deletedEvent);
            }
        }
    }
}

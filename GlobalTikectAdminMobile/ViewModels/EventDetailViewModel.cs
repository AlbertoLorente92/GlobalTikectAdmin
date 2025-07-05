using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Services;
using System.Collections.ObjectModel;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class EventDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Guid _id;
        [ObservableProperty]
        private string _name = default!;
        [ObservableProperty]
        private double _price;
        [ObservableProperty]
        private string _imageUrl;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CancelEventCommand))]
        private EventStatusEnum _eventStatus;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CancelEventCommand))]
        private DateTime _date = DateTime.Now;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private ObservableCollection<string> _artists = new();
        [ObservableProperty]
        private CategoryViewModel _category = new();
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowThumbnailImage))]
        private bool _showLargerImage;

        public bool ShowThumbnailImage => !ShowLargerImage;

        [RelayCommand(CanExecute = nameof(CanCancelEvent))]
        private async Task CancelEvent()
        {
            if(await _eventService.UpdateStatus(Id, EventStatusModel.Cancelled ))
            {
                EventStatus = EventStatusEnum.Cancelled;
            }
        }

        private bool CanCancelEvent() => EventStatus != EventStatusEnum.Cancelled && Date.AddHours(-4) > DateTime.Now;

        private readonly IEventService _eventService;

        public EventDetailViewModel(IEventService eventService)
        {
            _eventService = eventService;
            Id = Guid.Parse("EE272F8B-6096-4CB6-8625-BB4BB2D89E8B");
            GetEvent(Id);
        }

        private async void GetEvent(Guid id)
        {
            var @event = await _eventService.GetEventAsync(id);

            MapEventData(@event);
        }

        private void MapEventData(EventModel @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Price = @event.Price;
            ImageUrl = @event.ImageUrl;
            EventStatus = (EventStatusEnum)@event.Status;
            Date = @event.Date;
            Artists = @event.Artists.ToObservableCollection();
            Description = @event.Description;
            Category = new CategoryViewModel
            {
                Id = @event.Category.Id,
                Name = @event.Category.Name
            };
        }
    }
}

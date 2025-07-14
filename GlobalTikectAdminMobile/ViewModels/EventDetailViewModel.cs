using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GlobalTikectAdminMobile.Messages;
using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Services;
using GlobalTikectAdminMobile.ViewModels.Base;
using System.Collections.ObjectModel;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class EventDetailViewModel : ViewModelBase, IQueryAttributable
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
                WeakReferenceMessenger.Default.Send(new StatusChangedMessage(Id, EventStatus));
            }
        }

        [RelayCommand]
        private async Task NavigateToEditEvent()
        {
            var detailModel = MapToEventModel(this);
            await _navigationService.GoToEditEvent(detailModel);
        }

        [RelayCommand]
        private async Task DeleteEvent()
        {
            if (await _dialogService.Ask("Delete Event", "Are you sure you want to delete this event?"))
            {
                if (await _eventService.DeleteEvent(Id))
                {
                    WeakReferenceMessenger.Default.Send(new EventDeletedMessage(Id));
                    await _navigationService.GoToOverview();
                }
            }
        }

        private EventModel MapToEventModel(EventDetailViewModel eventDetailViewModel)
        {
            return new EventModel
            {
                Id = eventDetailViewModel.Id,
                Name = eventDetailViewModel.Name ?? string.Empty,
                Price = eventDetailViewModel.Price,
                ImageUrl = eventDetailViewModel.ImageUrl,
                Status = (EventStatusModel)eventDetailViewModel.EventStatus,
                Date = eventDetailViewModel.Date,
                Description = eventDetailViewModel.Description ?? string.Empty,
                Category = new CategoryModel
                {
                    Id = eventDetailViewModel.Category!.Id,
                    Name = eventDetailViewModel.Category.Name
                },
                Artists = eventDetailViewModel.Artists.ToList()
            };
        }

        private bool CanCancelEvent() => EventStatus != EventStatusEnum.Cancelled && Date.AddHours(-4) > DateTime.Now;

        private readonly IEventService _eventService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public EventDetailViewModel(IEventService eventService, INavigationService navigationService, IDialogService dialogService)
        {
            _eventService = eventService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public override async Task LoadAsync()
        {
            await Loading(async () =>
            {
                if (Id != Guid.Empty)
                {
                    await GetEvent(Id);
                }
            });
        }

        private async Task GetEvent(Guid id)
        {
            var @event = await _eventService.GetEventAsync(id);
            if (@event != null)
            {
                MapEventData(@event);
            }
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

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var eventId = query["EventId"].ToString();
            if (Guid.TryParse(eventId, out var selectedId))
            {
                Id = selectedId;
            }
        }
    }
}

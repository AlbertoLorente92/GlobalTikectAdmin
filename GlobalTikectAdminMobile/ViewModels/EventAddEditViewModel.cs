﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GlobalTikectAdminMobile.Messages;
using GlobalTikectAdminMobile.Models;
using GlobalTikectAdminMobile.Services;
using GlobalTikectAdminMobile.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class EventAddEditViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IEventService _eventService;
        private readonly ICategoryService _categoryService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public EventModel? eventDetail;

        [ObservableProperty]
        private string _pageTitle = default!;

        [ObservableProperty]
        private Guid _id;

        [ObservableProperty]
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        private string? _name;

        [ObservableProperty]
        //[Range(25,150)]
        [CustomValidation(typeof(EventAddEditViewModel), nameof(PriceValidation))]
        [NotifyDataErrorInfo]
        private double _price;

        public static ValidationResult PriceValidation(double price, ValidationContext context)
        {
            if (price < 25 || price > 150)
            {
                return new ValidationResult("Price must be between 25 and 150.");
            }
            return ValidationResult.Success!;
        }

        [ObservableProperty]
        private string? _imageUrl = null;

        [ObservableProperty]
        private EventStatusEnum _eventStatus;

        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private DateTime? _date = DateTime.Now;

        [ObservableProperty]
        [MaxLength(250)]
        [NotifyDataErrorInfo]
        private string? _description;

        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private CategoryViewModel? _category = new();

        public ObservableCollection<string> Artists { get; set; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddArtistCommand))]
        private string _addedArtist = default!;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Now;

        public ObservableCollection<ValidationResult> Errors { get; } = new();

        public List<EventStatusEnum> StatusList { get; set; } =
            Enum.GetValues(typeof(EventStatusEnum)).Cast<EventStatusEnum>().ToList();

        public ObservableCollection<CategoryViewModel> Categories { get; set; } = new();

        [RelayCommand(CanExecute = nameof(CanAddArtist))]
        private void AddArtist()
        {
            Artists.Add(AddedArtist);
            AddedArtist = string.Empty;
        }

        private bool CanAddArtist() => !string.IsNullOrWhiteSpace(AddedArtist);

        [RelayCommand(CanExecute = nameof(CanSubmitEvent))]
        private async Task Submit()
        {
            ValidateAllProperties();
            if (Errors.Any())
            {
                return;
            }

            EventModel model = MapDataToEventModel();

            if (Id == Guid.Empty)
            {
                if (await _eventService.CreateEvent(model))
                {
                    WeakReferenceMessenger.Default.Send(new EventAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The event is added.");
                    await _navigationService.GoToOverview();
                }
                else
                {
                    await _dialogService.Notify("Failed", "Adding the event failed.");
                }
            }
            else
            {
                if (await _eventService.EditEvent(model))
                {
                    WeakReferenceMessenger.Default.Send(new EventAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The event is updated.");
                    await _navigationService.GoBack();
                }
                else
                {
                    await _dialogService.Notify("Failed", "Editing the event failed.");
                }
            }
        }

        private EventModel MapDataToEventModel()
        {
            return new EventModel
            {
                Id = Id,
                Name = Name ?? string.Empty,
                Price = Price,
                ImageUrl = ImageUrl,
                Status = (EventStatusModel)EventStatus,
                Date = Date!.Value,
                Description = Description ?? string.Empty,
                Category = new CategoryModel
                {
                    Id = Category!.Id,
                    Name = Category.Name
                },
                Artists = Artists.ToList()
            };
        }

        private bool CanSubmitEvent() => !HasErrors;

        public EventAddEditViewModel(IEventService eventService, ICategoryService categoryService, INavigationService navigationService, IDialogService dialogService)
        {
            _eventService = eventService;
            _categoryService = categoryService;

            ErrorsChanged += AddEventViewModel_ErrorChanged;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        private void AddEventViewModel_ErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Errors.Clear();
            GetErrors().ToList().ForEach(Errors.Add);
            SubmitCommand.NotifyCanExecuteChanged();
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    var categories = await _categoryService.GetCategoriesAsync();
                    MapCategories(categories);

                    if (eventDetail is null && Id != Guid.Empty)
                    {
                        eventDetail = await _eventService.GetEventAsync(Id);
                    }
                    MapEvent(eventDetail);

                    ValidateAllProperties();
                });
        }

        private void MapCategories(List<CategoryModel> categories)
        {
            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel { Id = category.Id, Name = category.Name });
            }
        }

        private void MapEvent(EventModel? model)
        {
            if (model is not null)
            {
                Id = model.Id;
                Name = model.Name;
                Price = model.Price;
                ImageUrl = model.ImageUrl;
                EventStatus = (EventStatusEnum)model.Status;
                Date = model.Date;
                Description = model.Description;
                Category = Categories.FirstOrDefault(c => c.Id == model.Category.Id && c.Name == model.Category.Name);
                foreach (string artist in model.Artists)
                {
                    Artists.Add(artist);
                }
            }

            PageTitle = Id != Guid.Empty ? "Edit event" : "Add event";
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.Count > 0)
            {
                eventDetail = query["Event"] as EventModel;
            }
        }
    }
}

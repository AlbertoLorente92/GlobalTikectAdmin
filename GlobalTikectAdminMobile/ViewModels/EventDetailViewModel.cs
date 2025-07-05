using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        private List<string> _artists = new();
        [ObservableProperty]
        private CategoryViewModel _category = new();
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowThumbnailImage))]
        private bool _showLargerImage;

        public bool ShowThumbnailImage => !ShowLargerImage;

        [RelayCommand(CanExecute = nameof(CanCancelEvent))]
        private void CancelEvent() => EventStatus = EventStatusEnum.Cancelled;

        private bool CanCancelEvent() => EventStatus != EventStatusEnum.Cancelled && Date.AddHours(-4) > DateTime.Now;

        public EventDetailViewModel()
        {
            Id = Guid.Parse("EE272F8B-6096-4CB6-8625-BB4BB2D89E8B");
            Name = "John Egberts Live";
            Price = 65;
            ImageUrl = "https://lindseybroospluralsight.blob.core.windows.net/globoticket/images/banjo.jpg";
            EventStatus = EventStatusEnum.OnSale;
            Date = DateTime.Now.AddMonths(6);
            Description = "Join John for his farewell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.";
            Artists = new List<string> { "John Egbert", "Jane Egbert" };
            Category = new CategoryViewModel
            {
                Id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE"),
                Name = "Concert"
            };
        }
    }
}

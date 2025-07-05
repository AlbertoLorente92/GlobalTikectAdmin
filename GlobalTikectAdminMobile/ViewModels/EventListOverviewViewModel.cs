using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class EventListOverviewViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<EventListItemViewModel> _events = new();

        [ObservableProperty]
        private EventListItemViewModel? _selectedEvent;

        public EventListOverviewViewModel()
        {
            Events = new ObservableCollection<EventListItemViewModel>
            {
                new(Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                    "John Egbert Live",
                    65,
                    DateTime.Now.AddMonths(6),
                    new List<string> { "John Egbert", "Jane Egbert" },
                    EventStatusEnum.OnSale,
                    "https://lindseybroospluralsight.blob.core.windows.net/globoticket/images/banjo.jpg",
                    new CategoryViewModel
                    {
                        Id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE"),
                        Name = "Concert"
                    }),
                new(Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                    "The State of Affairs: Michael Live!", 85,
                    DateTime.Now.AddMonths(9),
                    new List<string> { "John Johnson" },
                    EventStatusEnum.OnSale,
                    "https://lindseybroospluralsight.blob.core.windows.net/globoticket/images/michael.jpg",
                    new CategoryViewModel
                    {
                        Id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE"),
                        Name = "Concert"
                    })
            };

            AddEvent();

            ChangeEvent();
        }

        private async void AddEvent()
        {
            await Task.Delay(5000);

            var @event = new EventListItemViewModel(Guid.Parse("{B419A7CA-3321-4F38-BE8E-4D7B6A529319}"),
                "Clash of the DJs",
                85,
                DateTime.Now.AddMonths(4), new List<string> { "DJ 'The Mike'" },
                EventStatusEnum.SalesClosed,
                "https://lindseybroospluralsight.blob.core.windows.net/globoticket/images/dj.jpg",
                new CategoryViewModel
                {
                    Id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE"),
                    Name = "Concert"
                });

            Events.Add(@event);

            
        }

        private async void ChangeEvent()
        {
            await Task.Delay(1000);
            
            var firstEvent = Events.FirstOrDefault();
            firstEvent.Price = 100;
            firstEvent.Name = "John Egbert Live - Updated";
        }
    }
}

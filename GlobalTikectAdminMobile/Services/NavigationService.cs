using GlobalTikectAdminMobile.Models;

namespace GlobalTikectAdminMobile.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoBack()
            => Shell.Current.GoToAsync("..");

        public async Task GoToAddEvent() => 
            await Shell.Current.GoToAsync("event/add");

        public async Task GoToEditEvent(EventModel detailModel)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Event", detailModel }
            };

            await Shell.Current.GoToAsync("event/edit", navigationParameter);
        }

        public Task GoToEventDetail(Guid id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "EventId", id }
            };
            return Shell.Current.GoToAsync("event", parameters);
        }

        public Task GoToOverview()
            => Shell.Current.GoToAsync("//overview");
    }
}

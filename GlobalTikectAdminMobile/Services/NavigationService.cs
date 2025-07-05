namespace GlobalTikectAdminMobile.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoToEventDetail(Guid id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "EventId", id }
            };
            return Shell.Current.GoToAsync("event", parameters);
        }
    }
}

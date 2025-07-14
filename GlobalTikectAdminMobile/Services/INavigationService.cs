using GlobalTikectAdminMobile.Models;

namespace GlobalTikectAdminMobile.Services
{
    public interface INavigationService
    {
        Task GoBack();
        Task GoToAddEvent();
        Task GoToEditEvent(EventModel detailModel);
        Task GoToEventDetail(Guid id);
        Task GoToOverview();
    }
}

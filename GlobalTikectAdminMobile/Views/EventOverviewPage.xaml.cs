using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventOverviewPage : ContentPageBase
{
    public EventOverviewPage(EventListOverviewViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
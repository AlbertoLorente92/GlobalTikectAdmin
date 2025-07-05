using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventOverviewPage : ContentPage
{
	public EventOverviewPage()
	{
		InitializeComponent();
		BindingContext = new EventListOverviewViewModel();
    }
}
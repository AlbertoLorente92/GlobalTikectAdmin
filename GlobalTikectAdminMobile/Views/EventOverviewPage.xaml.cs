using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventOverviewPage : ContentPage
{
	public EventOverviewPage(EventListOverviewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
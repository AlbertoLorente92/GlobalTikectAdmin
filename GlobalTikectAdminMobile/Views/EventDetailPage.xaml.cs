using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventDetailPage : ContentPage
{
	public EventDetailPage()
	{
		InitializeComponent();

		BindingContext = new EventDetailViewModel();
	}
}
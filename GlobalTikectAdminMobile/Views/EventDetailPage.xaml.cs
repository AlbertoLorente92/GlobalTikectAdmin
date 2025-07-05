using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventDetailPage : ContentPage
{
	public EventDetailPage(EventDetailViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}
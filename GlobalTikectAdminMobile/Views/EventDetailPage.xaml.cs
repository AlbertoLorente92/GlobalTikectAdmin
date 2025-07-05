using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventDetailPage : ContentPageBase
{
	public EventDetailPage(EventDetailViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}
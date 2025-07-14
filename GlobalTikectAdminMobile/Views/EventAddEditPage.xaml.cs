using GlobalTikectAdminMobile.ViewModels;

namespace GlobalTikectAdminMobile.Views;

public partial class EventAddEditPage : ContentPageBase
{
	public EventAddEditPage(EventAddEditViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
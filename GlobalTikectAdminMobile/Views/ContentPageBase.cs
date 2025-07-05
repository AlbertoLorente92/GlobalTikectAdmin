using GlobalTikectAdminMobile.ViewModels.Base;

namespace GlobalTikectAdminMobile.Views
{
    public class ContentPageBase : ContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is not IViewModelBase ivmb)
            {
                return;
            }

            await ivmb.InitializeAsyncCommand.ExecuteAsync(null);
        }
    }
}

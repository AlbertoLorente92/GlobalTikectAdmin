using CommunityToolkit.Mvvm.Input;

namespace GlobalTikectAdminMobile.ViewModels.Base
{
    public interface IViewModelBase
    {
        IAsyncRelayCommand InitializeAsyncCommand { get; }
    }
}

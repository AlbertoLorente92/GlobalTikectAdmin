using GlobalTikectAdminMobile.Views;

namespace GlobalTikectAdminMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("event", typeof(EventDetailPage));
        }
    }
}

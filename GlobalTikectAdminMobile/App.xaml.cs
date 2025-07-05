using GlobalTikectAdminMobile.Views;

namespace GlobalTikectAdminMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new EventOverviewPage();
        }
    }
}

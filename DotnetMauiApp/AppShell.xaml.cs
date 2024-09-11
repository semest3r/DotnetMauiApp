using DotnetMauiApp.Repositories;
using DotnetMauiApp.Views;

namespace DotnetMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Preferences.Clear();
            InitializeComponent();
            RegisterRoute();
        }

        static void RegisterRoute()
        {
            Routing.RegisterRoute(nameof(OnBoardingPage), typeof(OnBoardingPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(TransaksiPage), typeof(TransaksiPage));
            Routing.RegisterRoute(nameof(ResetDatabasePage), typeof(ResetDatabasePage));
        }
    }
}
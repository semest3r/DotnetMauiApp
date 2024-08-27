namespace DotnetMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var authenticated = false;
            if (authenticated)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new GuestShell();
            }
            Current.UserAppTheme = AppTheme.Light;
        }
    }
}

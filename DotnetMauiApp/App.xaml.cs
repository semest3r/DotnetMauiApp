namespace DotnetMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            InitializeComponent();
            MainPage = new AppShell();
            Preferences.Default.Clear();
            Current.UserAppTheme = AppTheme.Light;
        }

        private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Exception.ToString()}");
        }
    }
}

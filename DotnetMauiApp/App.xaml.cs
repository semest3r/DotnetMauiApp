using DotnetMauiApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DotnetMauiApp
{
    public partial class App : Application
    {
        private readonly DataContext _dataContext;
        public App(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.Migrate();

            //Exception
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            InitializeComponent();
            MainPage = new AppShell();
            
            //Default Theme
            Current.UserAppTheme = AppTheme.Light;
        }

        private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Exception.ToString()}");
        }
    }
}

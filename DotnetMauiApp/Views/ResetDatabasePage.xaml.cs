using DotnetMauiApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DotnetMauiApp.Views;

public partial class ResetDatabasePage : ContentPage
{
    private readonly DataContext _dataContext;
	public ResetDatabasePage(DataContext dataContext)
	{
        _dataContext = dataContext;
		InitializeComponent();
	}
    private async void Yes_Clicked(object sender, EventArgs e)
    {
        _dataContext.Database.ExecuteSqlRaw("Delete from 'transactions'");
        _dataContext.Database.ExecuteSqlRaw("Delete from 'wallets'");
        Preferences.Clear();
        await Shell.Current.GoToAsync($"../{nameof(OnBoardingPage)}");
    }
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}
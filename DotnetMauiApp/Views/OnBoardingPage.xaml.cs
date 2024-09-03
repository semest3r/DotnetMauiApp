using DotnetMauiApp.Services;

namespace DotnetMauiApp.Views;

public partial class OnBoardingPage : ContentPage
{
    private readonly AuthService _authService;
	public OnBoardingPage(AuthService authService)
	{
        _authService = authService;
		InitializeComponent();
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await Task.Delay(1); 
        if(await _authService.GetCurrentWalletId() == 0)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

}
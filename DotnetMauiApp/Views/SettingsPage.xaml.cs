namespace DotnetMauiApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
    }

    private async void NavigateToSetting_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ResetDatabasePage)}");
    }
}
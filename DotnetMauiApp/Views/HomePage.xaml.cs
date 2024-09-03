using CommunityToolkit.Maui.Views;
using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
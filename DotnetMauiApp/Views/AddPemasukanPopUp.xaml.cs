using CommunityToolkit.Maui.Views;
using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class AddPemasukanPopUp : Popup
{
	public AddPemasukanPopUp(PemasukanPopUpViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void Close_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}
using CommunityToolkit.Maui.Views;
using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class AddPemasukanPopUp : Popup
{
	public AddPemasukanPopUp(TransaksiViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Close_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}
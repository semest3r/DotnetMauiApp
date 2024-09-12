using CommunityToolkit.Maui.Views;
using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class EditTransaksiPopup : Popup
{
	public EditTransaksiPopup(EditTransaksiPopupViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
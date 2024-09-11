using CommunityToolkit.Maui.Views;

namespace DotnetMauiApp.Views;

public partial class EditTransaksiPopup : Popup
{
	public EditTransaksiPopup()
	{
		InitializeComponent();
	}
    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
using CommunityToolkit.Maui.Views;

namespace DotnetMauiApp.Views;

public partial class AddPengeluaranPopUp : Popup
{
	public AddPengeluaranPopUp()
	{
		InitializeComponent();
	}

    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
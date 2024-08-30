using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class TransaksiPage : ContentPage
{
	public TransaksiPage(TransaksiViewModel transaksiViewModel)
	{
		InitializeComponent();
		BindingContext = transaksiViewModel;
		transaksiViewModel.TransaksiAll();
	}
}
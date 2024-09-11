using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class TransaksiPage : ContentPage
{
	public TransaksiPage(TransactionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
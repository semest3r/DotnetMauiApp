using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views;

public partial class TransaksiPage : ContentPage
{
	public TransaksiPage(TransaksiViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        SentrySdk.CaptureMessage("Hello Sentry");
    }
}
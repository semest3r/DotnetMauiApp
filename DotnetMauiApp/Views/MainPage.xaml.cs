using CommunityToolkit.Maui.Views;
using DotnetMauiApp.ViewModels;

namespace DotnetMauiApp.Views
{
    public partial class MainPage : ContentPage
    {

        private readonly TransaksiViewModel _viewModel;
        public MainPage(TransaksiViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
            if(_viewModel.TransaksiSource.Count < 1)
            {
                _viewModel.TransaksiAll();
            }
        }

        private void Pemasukan_Clicked(object sender, EventArgs e)
        {
            var popup = new AddPemasukanPopUp(_viewModel);
            this.ShowPopup(popup);
        }

        private void Pengeluaran_Clicked(object sender, EventArgs e)
        {
            var popup = new AddPengeluaranPopUp();
            this.ShowPopup(popup);
        }
    }

}

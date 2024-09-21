using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Views;

namespace DotnetMauiApp.ViewModels
{
    public partial class RegisterViewModel: ObservableObject
    {

        private readonly WalletRepository _walletRepository;

        public RegisterViewModel(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [ObservableProperty]
        string walletName;

        [RelayCommand]
        async Task RegisterWallet()
        {
            var wallet = new Wallet
            {
                Name = WalletName,
                TotalMoney = 0,
            };

            var createdWallet = await _walletRepository.AddWallet(wallet);
            Preferences.Default.Set("wallet", createdWallet);
            ClearForm();
            await RedirectToHome();
        }

        async Task RedirectToHome()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        void ClearForm()
        {
            WalletName = string.Empty;
        }
    }
}

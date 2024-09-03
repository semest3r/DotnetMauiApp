﻿using CommunityToolkit.Mvvm.ComponentModel;
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
                TotalUang = 0,
            };

            var createdWalelt = await _walletRepository.AddNewWallet(wallet);
            Preferences.Default.Set("wallet", createdWalelt);
            WalletName = string.Empty;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}

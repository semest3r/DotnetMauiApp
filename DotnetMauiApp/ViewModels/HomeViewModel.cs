using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace DotnetMauiApp.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        readonly TransactionRepository _transaksiRepository;
        readonly WalletRepository _walletRepository;
        readonly IPopupService _popupService;

        public HomeViewModel(AuthService authService,
                             TransactionRepository transaksiRepository,
                             WalletRepository walletRepository,
                             IPopupService popupService) : base(authService)
        {
            _popupService = popupService;
            _transaksiRepository = transaksiRepository;
            _walletRepository = walletRepository;
            RecentTransactionResource = [];
            Wallets = [];
            CurrentWallet();
            AllWallets();
        }

        [ObservableProperty]
        ObservableCollection<Wallet> wallets;
        public async void AllWallets()
        {
            var wallets = await _walletRepository.GetAllWallet();
            Wallets.Clear();
            foreach (var item in wallets)
            {
                Wallets.Add(item);
            }
        }

        [ObservableProperty]
        Wallet selectedWallet;
        public async void CurrentWallet()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var wallet = await _walletRepository.GetWalletById(walletId);
            SelectedWallet = wallet;
        }
        partial void OnSelectedWalletChanged(Wallet value)
        {
            Preferences.Set("wallet", SelectedWallet.Id);
            RecentTransaksi();
        }

        [ObservableProperty]
        ObservableCollection<Transaction> recentTransactionResource;
        public async void RecentTransaksi()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var transaksiAll = await _transaksiRepository.GetRecentTransaction(walletId);
            RecentTransactionResource.Clear();
            foreach (var item in transaksiAll)
            {
                RecentTransactionResource.Add(item);
            }
        }

        [RelayCommand]
        async Task ShowAddPemasukanPopup()
        {
            await _popupService.ShowPopupAsync<PemasukanPopUpViewModel>();
        }

        [RelayCommand]
        async Task ShowAddPengeluaranPopup()
        {
            await _popupService.ShowPopupAsync<PengeluaranPopUpViewModel>();
        }

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        void RefreshData()
        {
            IsBusy = true;

            AllWallets();
            CurrentWallet();
            RecentTransaksi();

            IsBusy = false;
        }

    }
}


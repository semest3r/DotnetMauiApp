using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;


namespace DotnetMauiApp.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        readonly TransactionRepository _transactionRepository;
        readonly WalletRepository _walletRepository;
        readonly IPopupService _popupService;

        public HomeViewModel(AuthService authService,
                             TransactionRepository transactionRepository,
                             WalletRepository walletRepository,
                             IPopupService popupService) : base(authService)
        {
            _popupService = popupService;
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            RecentTransactionResource = [];
            Wallets = [];
            SetSelectedWallet();
            SetWallets();
            SetRecentTransaction();
        }

        [ObservableProperty]
        ObservableCollection<Wallet> wallets;
        public async void SetWallets()
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
        public async void SetSelectedWallet()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var wallet = await _walletRepository.GetWalletById(walletId);
            SelectedWallet = wallet;
        }
        partial void OnSelectedWalletChanged(Wallet? oldValue, Wallet newValue)
        {
            if(newValue != null)
            {
                Preferences.Default.Set("wallet", newValue.Id);
                RefreshData();
            }
        }

        [ObservableProperty]
        ObservableCollection<Transaction> recentTransactionResource;
        public async void SetRecentTransaction()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var transactions = await _transactionRepository.GetRecentTransaction(walletId);
            RecentTransactionResource.Clear();
            foreach (var item in transactions)
            {
                RecentTransactionResource.Add(item);
            }
        }

        [RelayCommand]
        async Task ShowAddDepositPopup()
        {
            await _popupService.ShowPopupAsync<PemasukanPopUpViewModel>();
        }

        [RelayCommand]
        async Task ShowAddWithdrawPopup()
        {
            await _popupService.ShowPopupAsync<PengeluaranPopUpViewModel>();
        }

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        void RefreshData()
        {
            IsBusy = true;
            SetWallets();
            SetSelectedWallet();
            SetRecentTransaction();
            IsBusy = false;
        }

    }
}


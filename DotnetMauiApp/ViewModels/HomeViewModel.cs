using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System.Collections.ObjectModel;


namespace DotnetMauiApp.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        readonly TransaksiRepository _transaksiRepository;
        readonly WalletRepository _walletRepository;
        readonly IPopupService _popupService;

        public HomeViewModel(AuthService authService,
                             TransaksiRepository transaksiRepository,
                             WalletRepository walletRepository,
                             IPopupService popupService) : base(authService)
        {
            _popupService = popupService;
            _transaksiRepository = transaksiRepository;
            _walletRepository = walletRepository;
            RecentTransaksiResource = [];
            CurrentWallet();
            RecentTransaksi();
        }

        public async void CurrentWallet()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var wallet = await _walletRepository.GetById(walletId);
            Wallet = new Wallet
            {
                Id = wallet.Id,
                Name = wallet.Name,
                TotalUang = wallet.TotalUang,
            };
        }

        public async void RecentTransaksi()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var transaksiAll = await _transaksiRepository.GetRecentTransaksi(walletId);

            RecentTransaksiResource.Clear();
            foreach (var item in transaksiAll)
            {
                RecentTransaksiResource.Add(item);
            }
        }

        [ObservableProperty]
        ObservableCollection<Transaksi> recentTransaksiResource;

        [ObservableProperty]
        Wallet wallet;

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

            CurrentWallet();
            RecentTransaksi();

            IsBusy = false;
        }
    }
}


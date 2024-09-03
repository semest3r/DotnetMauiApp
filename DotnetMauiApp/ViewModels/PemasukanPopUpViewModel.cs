using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;

namespace DotnetMauiApp.ViewModels
{
    public partial class PemasukanPopUpViewModel : BaseViewModel
    {

        readonly TransaksiRepository _transaksiRepository;
        readonly WalletRepository _walletRepository;

        public PemasukanPopUpViewModel(AuthService authService,
                                       TransaksiRepository transaksiRepository,
                                       WalletRepository walletRepository) : base(authService)
        {
            _transaksiRepository = transaksiRepository;
            _walletRepository = walletRepository;
        }

        [ObservableProperty]
        string deskripsi;

        [ObservableProperty]
        double jumlahUang;

        [RelayCommand]
        async Task AddPemasukan()
        {
            if (string.IsNullOrEmpty(Deskripsi))
            {
                return;
            }
            if (double.IsNaN(JumlahUang))
            {
                return;
            }
            var walletId = await _authService.GetCurrentWalletId();
            var currentWallet = await _walletRepository.GetById(walletId);

            var transaksi = new Transaksi
            {
                Description = Deskripsi,
                JumlahUang = JumlahUang,
                CreatedAt = DateTime.Now,
                TipeTransaksi = "In",
                WalletId = await _authService.GetCurrentWalletId(),
            };

            var wallet = new Wallet
            {
                Id = await _authService.GetCurrentWalletId(),
                TotalUang = currentWallet.TotalUang + JumlahUang,
            };

            await _transaksiRepository.AddTransaksi(transaksi);
            await _walletRepository.UpdateWallet(wallet);

            Deskripsi = string.Empty;
            JumlahUang = 0;
        }
    }
}

using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;

namespace DotnetMauiApp.ViewModels
{
    public partial class PengeluaranPopUpViewModel : BaseViewModel
    {

        readonly TransactionRepository _transactionRepository;
        readonly WalletRepository _walletRepository;
 
        public PengeluaranPopUpViewModel(AuthService authService, TransactionRepository transactionRepository, WalletRepository walletRepository) : base(authService)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        [ObservableProperty]
        string deskripsi;

        [ObservableProperty]
        double totalMoney;

        [RelayCommand]
        async Task AddPengeluaran()
        {
            if (string.IsNullOrEmpty(Deskripsi))
            {
                return;
            }
            if (double.IsNaN(TotalMoney))
            {
                return;
            }

            var walletId = await _authService.GetCurrentWalletId();
            var currentWallet = await _walletRepository.GetWalletById(walletId);
            var currentTotalMoney = currentWallet.TotalMoney - TotalMoney;

            var transaction = new Transaction
            {
                Description = Deskripsi,
                TotalMoney = TotalMoney,
                CurrrentTotalMoney = TotalMoney,
                CreatedAt = DateTime.Now,
                TypeTransaction = "Out",
                WalletId = currentWallet.Id,
            };

            var updateWallet = new Wallet
            {
                Id = currentWallet.Id,
                Name = currentWallet.Name,
                TotalMoney = currentWallet.TotalMoney - TotalMoney,
            };

            try
            {
                await _transactionRepository.AddTransaction(transaction);
                await _walletRepository.UpdateWallet(currentWallet, updateWallet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Message}");
            }

            Deskripsi = string.Empty;
            TotalMoney = 0;
        }

    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System.ComponentModel.DataAnnotations;

namespace DotnetMauiApp.ViewModels
{
    public partial class PemasukanPopUpViewModel : BaseViewModel
    {
        readonly TransactionRepository _transactionRepository;
        readonly WalletRepository _walletRepository;

        public PemasukanPopUpViewModel(AuthService authService,
                                       TransactionRepository transactionRepository,
                                       WalletRepository walletRepository) : base(authService)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        [ObservableProperty]
        string description;

        [ObservableProperty]
        double totalMoney;

        [RelayCommand]
        async Task AddPemasukan()
        {
            if (string.IsNullOrEmpty(Description))
            {
                // return validation error
                return;
            }
            if (double.IsNaN(TotalMoney))
            {
                // return validation error
                return;
            }

            var walletId = await _authService.GetCurrentWalletId();
            var currentWallet = await _walletRepository.GetWalletById(walletId);
            var currentTotalMoney = currentWallet.TotalMoney + TotalMoney;
            var transaction = new Transaction
            {
                Description = Description,
                TotalMoney = TotalMoney,
                CurrrentTotalMoney = TotalMoney,
                CreatedAt = DateTime.Now,
                TypeTransaction = "In",
                WalletId = walletId,
            };

            var updateWallet = new Wallet
            {
                Id = currentWallet.Id,
                Name = currentWallet.Name,
                TotalMoney = currentTotalMoney,
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

            Description = string.Empty;
            TotalMoney = 0;
        }
    }
}

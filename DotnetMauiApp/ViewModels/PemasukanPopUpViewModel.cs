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
        readonly TransactionService _transactionService;
        readonly WalletRepository _walletRepository;

        public PemasukanPopUpViewModel(TransactionService transactionService, WalletRepository walletRepository,AuthService authService) : base(authService)
        {
            _transactionService = transactionService;
            _walletRepository = walletRepository;
        }

        [ObservableProperty]
        Deposit deposit;

        [RelayCommand]
        async Task AddPemasukan()
        {
            if (string.IsNullOrEmpty(Deposit.Description))
            {
                // return validation error
                return;
            }
            if (double.IsNaN(Deposit.TotalMoney))
            {
                // return validation error
                return;
            }

            var walletId = await _authService.GetCurrentWalletId();
            var currentWallet = await _walletRepository.GetWalletById(walletId);
            var currentTotalMoney = currentWallet.TotalMoney + Deposit.TotalMoney;

            var transaction = new Transaction
            {
                Description = Deposit.Description,
                TotalMoney = Deposit.TotalMoney,
                CurrrentTotalMoney = currentTotalMoney,
                CreatedAt = DateTime.Now,
                TypeTransaction = "In",
                WalletId = currentWallet.Id,
            };

            var updateWallet = new Wallet
            {
                Id = currentWallet.Id,
                Name = currentWallet.Name,
                TotalMoney = currentWallet.TotalMoney - Deposit.TotalMoney,
            };

            await _transactionService.AddTransactionAndUpdateWallet(transaction, currentWallet, updateWallet);

            Deposit.Description = string.Empty;
            Deposit.TotalMoney = 0;
        }
    }
}

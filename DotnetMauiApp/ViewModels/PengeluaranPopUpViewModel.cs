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

        readonly TransactionService _transactionService;
        readonly WalletRepository _walletRepository;
 
        public PengeluaranPopUpViewModel(TransactionService transactionService, WalletRepository walletRepository,AuthService authService) : base(authService)
        {
            _transactionService = transactionService;
            _walletRepository = walletRepository;
        }

        [ObservableProperty]
        Withdraw withdraw;

        [RelayCommand]
        async Task AddPengeluaran()
        {
            if (string.IsNullOrEmpty(Withdraw.Description))
            {
                return;
            }
            if (double.IsNaN(Withdraw.TotalMoney))
            {
                return;
            }

            var walletId = await _authService.GetCurrentWalletId();
            var currentWallet = await _walletRepository.GetWalletById(walletId);
            var currentTotalMoney = currentWallet.TotalMoney - Withdraw.TotalMoney;

            var transaction = new Transaction
            {
                Description = Withdraw.Description,
                TotalMoney = Withdraw.TotalMoney,
                CurrrentTotalMoney = currentTotalMoney,
                CreatedAt = DateTime.Now,
                TypeTransaction = "Out",
                WalletId = currentWallet.Id,
            };

            var updateWallet = new Wallet
            {
                Id = currentWallet.Id,
                Name = currentWallet.Name,
                TotalMoney = currentWallet.TotalMoney - Withdraw.TotalMoney,
            };

            await _transactionService.AddTransactionAndUpdateWallet(transaction, currentWallet, updateWallet);

            ClearForm();
        }

        void ClearForm()
        {
            Withdraw.Description = string.Empty;
            Withdraw.TotalMoney = 0;
        }

    }
}

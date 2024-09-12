using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetMauiApp.ViewModels
{
    public partial class EditTransaksiPopupViewModel : BaseViewModel
    {
        private readonly WalletRepository _walletRepository;
        private readonly TransactionRepository _transactionRepository;

        public EditTransaksiPopupViewModel(WalletRepository walletRepository, TransactionRepository transactionRepository, AuthService authService):base(authService)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        [ObservableProperty]
        Transaction transaction;

        [RelayCommand]
        async Task EditTransaksiAsync()
        {
            if (string.IsNullOrEmpty(Transaction.Description))
            {
                return;
            }
            if (double.IsNaN(Transaction.TotalMoney))
            {
                return;
            }

            var currentWallet = await _walletRepository.GetWalletById(Transaction.WalletId);
            var oldTransaction = await _transactionRepository.GetTransactionById(Transaction.Id);
            var newTotalMoney = Transaction.TypeTransaction == "In" 
                ? currentWallet.TotalMoney - (oldTransaction.TotalMoney - Transaction.TotalMoney) 
                : currentWallet.TotalMoney + (oldTransaction.TotalMoney - Transaction.TotalMoney);

            var newCurrentTotalMoney = Transaction.TypeTransaction == "In"
                ? Transaction.CurrrentTotalMoney - (oldTransaction.TotalMoney - Transaction.TotalMoney)
                : Transaction.CurrrentTotalMoney + (oldTransaction.TotalMoney - Transaction.TotalMoney);
            
            var updateTransaction = new Transaction
            {
                Id = Transaction.Id,
                Description = Transaction.Description,
                TotalMoney = Transaction.TotalMoney,
                CurrrentTotalMoney = newCurrentTotalMoney,
                CreatedAt = Transaction.CreatedAt,
                TypeTransaction = Transaction.TypeTransaction,
                WalletId = Transaction.WalletId,
            };
            System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {newTotalMoney}");

            var updateWallet = new Wallet
            {
                Id = currentWallet.Id,
                Name = currentWallet.Name,
                TotalMoney = newTotalMoney,
            };

            try
            {
                await _transactionRepository.UpdateTransaction(oldTransaction, updateTransaction);
                await _walletRepository.UpdateWallet(currentWallet, updateWallet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Message}");
            }
        }
    }
}

using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetMauiApp.Services
{
    public class TransactionService
    {
        private readonly WalletRepository _walletRepository;
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(WalletRepository walletRepository, TransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task UpdateTransactionAndWallet(Transaction transaction)
        {
            var currentWallet = await _walletRepository.GetWalletById(transaction.WalletId);
            var oldTransaction = await _transactionRepository.GetTransactionById(transaction.Id);

            var newTotalMoney = TransactionService.CalcTotalMoney(transaction, currentWallet.TotalMoney, oldTransaction.TotalMoney);
            var newCurrentTotalMoney = TransactionService.CalcTotalMoney(transaction, transaction.CurrrentTotalMoney, oldTransaction.TotalMoney);

            var updateTransaction = new Transaction
            {
                Id = transaction.Id,
                Description = transaction.Description,
                TotalMoney = transaction.TotalMoney,
                CurrrentTotalMoney = newCurrentTotalMoney,
                CreatedAt = transaction.CreatedAt,
                TypeTransaction = transaction.TypeTransaction,
                WalletId = transaction.WalletId,
            };

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

        private static double CalcTotalMoney(Transaction transaction, double currentMoney, double oldMoney)
        {
            var newTotalMoney = transaction.TypeTransaction == "In"
                ? currentMoney - (oldMoney - transaction.TotalMoney)
                : currentMoney + (oldMoney - transaction.TotalMoney);

            return newTotalMoney;
        }

        public async Task AddTransactionAndUpdateWallet(Transaction transaction, Wallet wallet, Wallet updateWallet)
        {
            try
            {
                await _transactionRepository.AddTransaction(transaction);
                await _walletRepository.UpdateWallet(wallet, updateWallet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Message}");
            }
        }
    }
}

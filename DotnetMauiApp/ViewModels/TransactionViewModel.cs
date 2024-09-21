using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System.Collections.ObjectModel;

namespace DotnetMauiApp.ViewModels
{
    public partial class TransactionViewModel: BaseViewModel
    {
        private readonly TransactionService _transactionService;
        private readonly TransactionRepository _transactionRepository; 
        private readonly WalletRepository _walletRepository;
        readonly IPopupService _popupService;
        public TransactionViewModel(AuthService authService,
                                  TransactionRepository transactionRepository,
                                  TransactionService transactionService,
                                  WalletRepository walletRepository,
                                  IPopupService popupService) : base(authService) 
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _popupService = popupService;
            TransactionSource = [];
            DateTime date = DateTime.UtcNow;
            DateFrom = new DateTime(date.Year, date.Month, 1, 23, 59, 59);
            DateTo = DateTime.UtcNow;
            SetTransactions();
            SetTotalDeposit();
        }

        [ObservableProperty]
        ObservableCollection<Transaction> transactionSource;
        public async Task SetTransactions()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var transaksiAll = await _transactionRepository.GetAllTransaction(walletId, DateFrom, DateTo);
            TransactionSource.Clear();
            foreach(var item in transaksiAll)
            {
                TransactionSource.Add(item);
            }
        }

        [ObservableProperty]
        double totalDeposit;
        public async Task SetTotalDeposit()
        {
            var walletId = await _authService.GetCurrentWalletId();
            try
            {
                var totalDeposit = _transactionRepository.GetTotalDespoit(walletId, DateFrom, DateTo);
                TotalDeposit = totalDeposit;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {ex.Message}");
            }

        }

        [ObservableProperty]
        double totalWithdraw;
        public async Task SetTotalWithdraw()
        {
            var walletId = await _authService.GetCurrentWalletId();
            try
            {
                var totalWithdraw = _transactionRepository.GetTotalWithdraw(walletId, DateFrom, DateTo);
                TotalWithdraw = totalWithdraw;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {ex.Message}");
            }
        }

        [ObservableProperty]
        DateTime dateFrom;
        async partial void OnDateFromChanged(DateTime value)
        {
            await RefreshData();
        }

        [ObservableProperty]
        DateTime dateTo;
        async partial void OnDateToChanged(DateTime value)
        {
            await RefreshData();
        }

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        async Task RefreshData()
        {
            IsBusy = true;
            await SetTransactions();
            await SetTotalDeposit();
            await SetTotalWithdraw();
            IsBusy = false;
        }

        [RelayCommand]
        private async Task DeleteTransaksiAsync(Transaction transaction)
        {
            var wallet = await _walletRepository.GetWalletById(transaction.WalletId);
            await _transactionService.DeleteTransactionAndUpdateWallet(transaction, wallet);
            await RefreshData();
        }

        [RelayCommand]
        async Task EditTransaksiPopup(Transaction transaksi)
        {
            try
            {
                await _popupService.ShowPopupAsync<EditTransaksiPopupViewModel>(onPresenting: vm => vm.Transaction = transaksi);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {ex.Message}");
            }
        }
    }
}

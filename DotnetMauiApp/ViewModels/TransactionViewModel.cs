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
        private readonly TransactionRepository _transactionRepository;
        private readonly WalletRepository _walletRepository;
        readonly IPopupService _popupService;
        public TransactionViewModel(AuthService authService,
                                  TransactionRepository transactionRepository,
                                  WalletRepository walletRepository,
                                  IPopupService popupService) : base(authService) 
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _popupService = popupService;
            TransactionSource = [];
            DateTime date = DateTime.UtcNow;
            DateFrom = new DateTime(date.Year, date.Month, 1, 23, 59, 59);
            DateTo = DateTime.UtcNow;
            TransaksiAll();
            GetTotalPemasukan();
        }

        [ObservableProperty]
        ObservableCollection<Transaction> transactionSource;
        public async Task TransaksiAll()
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
        double totalPemasukan;
        public async Task GetTotalPemasukan()
        {
            var walletId = await _authService.GetCurrentWalletId();
            try
            {
                var totalPemasukan = _transactionRepository.GetTotalDespoit(walletId, DateFrom, DateTo);
                TotalPemasukan = totalPemasukan;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {ex.Message}");
            }

        }

        [ObservableProperty]
        double totalPengeluaran;
        public async Task GetTotalPengeluaran()
        {
            var walletId = await _authService.GetCurrentWalletId();
            try
            {
                var totalPengeluaran = _transactionRepository.GetTotalWithdraw(walletId, DateFrom, DateTo);
                TotalPengeluaran = totalPengeluaran;
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
            await GetTotalPemasukan();
            await GetTotalPengeluaran();
        }

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        async Task RefreshData()
        {
            IsBusy = true;
            await TransaksiAll();
            IsBusy = false;
        }

        [RelayCommand]
        private async Task DeleteTransaksiAsync(Transaction transaction)
        {
            var wallet = await _walletRepository.GetWalletById(transaction.WalletId);
            await _transactionRepository.DeleteTransaction(transaction);
            var updateWallet = new Wallet
            {
                Id = transaction.WalletId,
                Name = wallet.Name,
                TotalMoney = transaction.TypeTransaction == "In" ? wallet.TotalMoney - transaction.TotalMoney : wallet.TotalMoney + transaction.TotalMoney,
            };
            await _walletRepository.UpdateWallet(wallet, updateWallet);
            await TransaksiAll();
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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using DotnetMauiApp.Services;
using System.Collections.ObjectModel;

namespace DotnetMauiApp.ViewModels
{
    public partial class TransaksiViewModel: BaseViewModel
    {
        private readonly TransaksiRepository _transaksiRepository;
        public TransaksiViewModel(AuthService authService, TransaksiRepository transaksiRepository) : base(authService) 
        {
            _transaksiRepository = transaksiRepository;
            LokasiFileSystem = Path.Combine(FileSystem.AppDataDirectory);
            TransaksiSource = [];
            DateTime date = DateTime.UtcNow;
            DateFrom = new DateTime(date.Year, date.Month, 1);
            DateTo = DateTime.UtcNow;
            TransaksiAll();
            GetTotalPemasukan();
        }

        public async Task TransaksiAll()
        {
            var walletId = await _authService.GetCurrentWalletId();
            var transaksiAll = await _transaksiRepository.GetAll(walletId, DateFrom, DateTo);
            TransaksiSource.Clear();
            foreach(var item in transaksiAll)
            {
                TransaksiSource.Add(item);
            }
        }

        [ObservableProperty]
        double totalPemasukan;
        public async Task GetTotalPemasukan()
        {
            var walletId = await _authService.GetCurrentWalletId();
            try
            {
                var totalPemasukan = await _transaksiRepository.GetTotalPemasukan(walletId);
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
                var totalPengeluaran = await _transaksiRepository.GetTotalPengeluaran(walletId);
                TotalPengeluaran = totalPengeluaran;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {ex.Message}");
            }
        }

        [ObservableProperty]
        DateTime dateFrom;
        partial void OnDateFromChanged(DateTime value)
        {
            RefreshData();
        }

        [ObservableProperty]
        DateTime dateTo;
        partial void OnDateToChanged(DateTime value)
        {
            RefreshData();
        }

        [ObservableProperty]
        ObservableCollection<Transaksi> recentTransaksiSource;

        [ObservableProperty]
        ObservableCollection<Transaksi> transaksiSource;

        [ObservableProperty]
        string lokasiFileSystem;

        [ObservableProperty]
        bool isBusy;

        [RelayCommand]
        void RefreshData()
        {
            IsBusy = true;
            TransaksiAll();
            IsBusy = false;
        }

    }
}

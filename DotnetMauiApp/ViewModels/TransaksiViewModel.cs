using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotnetMauiApp.Models;
using DotnetMauiApp.Repositories;
using System.Collections.ObjectModel;

namespace DotnetMauiApp.ViewModels
{
    public partial class TransaksiViewModel: ObservableObject
    {
        private readonly TransaksiRepository _transaksiRepository;
        public TransaksiViewModel(TransaksiRepository transaksiRepository)
        {
            _transaksiRepository = transaksiRepository;
            TransaksiSource = [];
            RecentTransaksiSource = [];
            LokasiFileSystem = Path.Combine(FileSystem.AppDataDirectory);
        }
        public async void TransaksiAll()
        {
            var transaksiAll = await _transaksiRepository.GetAll();
            TransaksiSource.Clear();
            foreach(var item in transaksiAll)
            {
                TransaksiSource.Add(item);
            }
        }
        public async void RecentTransaksi()
        {
            var transaksiAll = await _transaksiRepository.GetRecentTransaksi();
            RecentTransaksiSource.Clear();
            foreach (var item in transaksiAll)
            {
                RecentTransaksiSource.Add(item);
            }
        }

        [ObservableProperty]
        ObservableCollection<Transaksi> recentTransaksiSource;

        [ObservableProperty]
        ObservableCollection<Transaksi> transaksiSource;

        [ObservableProperty]
        string lokasiFileSystem;
        [ObservableProperty]
         string deskripsi;

        [ObservableProperty]
         double jumlahUang;

        [ObservableProperty]
         string tipeTransaksi;
        
        [RelayCommand]
        async void AddPemasukan()
        {
            if (string.IsNullOrEmpty(Deskripsi))
            {
                return;
            }
            if(double.IsNaN(JumlahUang))
            {
                return;
            }
            var transaksi = new Transaksi
            {
                Description = Deskripsi,
                JumlahUang = JumlahUang,
                CreatedAt = DateTime.Now,
                TipeTransaksi = "In",
            };

            await _transaksiRepository.AddTransaksi(transaksi);
            TransaksiAll();
            RecentTransaksi();

            Deskripsi = string.Empty;
            JumlahUang = 0;
        }

        [RelayCommand]
        async void AddPengeluaran()
        {
            if (string.IsNullOrEmpty(Deskripsi))
            {
                return;
            }
            if (double.IsNaN(JumlahUang))
            {
                return;
            }
            var transaksi = new Transaksi
            {
                Description = Deskripsi,
                JumlahUang = JumlahUang,
                CreatedAt = DateTime.Now,
                TipeTransaksi = "Out"
            };

            await _transaksiRepository.AddTransaksi(transaksi);
            TransaksiAll();
            RecentTransaksi();

            Deskripsi = string.Empty;
            JumlahUang = 0;
        }
    }
}

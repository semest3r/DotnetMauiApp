using DotnetMauiApp.Data;
using DotnetMauiApp.Models;
using SQLite;


namespace DotnetMauiApp.Repositories
{
    public class TransaksiRepository
    {
        SQLiteAsyncConnection conn;

        async Task Init()
        {
            if (conn != null)
            {
                return;
            }
            conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await conn.CreateTableAsync<Transaksi>();
        }

        public async Task<int> AddTransaksi(Transaksi transaksi)
        {
            await Init();
            int result = await conn.InsertAsync(transaksi);
            return result;
        }

        public async Task<List<Transaksi>> GetAll(int walletId, DateTime dateFrom, DateTime dateTo)
        {
            await Init();
            List<Transaksi> list = await conn.Table<Transaksi>()
                .Where(x => x.WalletId == walletId && x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                .ToListAsync();
            return list;
        }

        public async Task<List<Transaksi>> GetRecentTransaksi(int WalletId)
        {
            await Init();
            List<Transaksi> list = await conn.Table<Transaksi>()
                .Take(10)
                .Where(x=> x.WalletId == WalletId)
                .OrderBy(x => x.Id)
                .ToListAsync();
            return list;
        }

        public async Task<Transaksi> GetById(int id)
        {
            await Init();
            var transaksi = await conn.Table<Transaksi>()
                .FirstOrDefaultAsync(x => x.Id == id);
            return transaksi;
        }

        public async Task<double> GetTotalPemasukan(int walletId)
        {
            var transaksi = await conn.QueryAsync<Transaksi>("SELECT SUM(transaksi.JumlahUang) AS TotalUang FROM transaksi WHERE transaksi.WalletId == ? AND transaksi.TipeTransaksi == 'In' GROUP BY transaksi.TipeTransaksi", walletId);
            if (transaksi.Count < 1)
            {
                return 0;
            }
            var result = transaksi[0];
            return result.TotalUang;
        }

        public async Task<double> GetTotalPengeluaran(int walletId)
        {
            var transaksi = await conn.QueryAsync<Transaksi>("SELECT SUM(transaksi.JumlahUang) AS TotalUang FROM transaksi WHERE transaksi.WalletId == ? transaksi.TipeTransaksi == 'Out' GROUP BY transaksi.TipeTransaksi", walletId);
            if( transaksi.Count < 1)
            {
                return 0;
            }
            var result = transaksi[0];
            return result.TotalUang;
        }

        public async Task<int> UpdateTransaksi(Transaksi transaksi)
        {
            await Init();
            int result = await conn.UpdateAsync(transaksi);
            return result;
        }

        public async Task<int> DeleteTransaksi(int id)
        {
            await Init();
            int result = await conn.DeleteAsync<Transaksi>(id);
            return result;
        }
    }
}

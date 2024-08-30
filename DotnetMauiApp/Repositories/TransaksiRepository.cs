using DotnetMauiApp.Data;
using DotnetMauiApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Transaksi>> GetAll()
        {
            await Init();
            List<Transaksi> list = await conn.Table<Transaksi>().ToListAsync();
            return list;
        }

        public async Task<List<Transaksi>> GetRecentTransaksi()
        {
            await Init();
            List<Transaksi> list = await conn.Table<Transaksi>().Take(10).OrderBy(x => x.Id).ToListAsync();
            return list;
        }

        public async Task<Transaksi> GetById(int id)
        {
            await Init();
            var transaksi = await conn.Table<Transaksi>().FirstOrDefaultAsync(x => x.Id == id);
            return transaksi;
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

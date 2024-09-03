using DotnetMauiApp.Data;
using DotnetMauiApp.Models;
using SQLite;

namespace DotnetMauiApp.Repositories
{
    public class WalletRepository
    {
        SQLiteAsyncConnection conn;

        async Task Init()
        {
            if (conn != null)
            {
                return;
            }
            conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await conn.CreateTableAsync<Wallet>();
        }
        public  async Task<int> AddNewWallet(Wallet wallet)
        {
            await Init();
            int result = await conn.InsertAsync(wallet);
            return result;
        }

        public async Task<List<Wallet>> GetAll()
        {
            await Init();
            List<Wallet> list = await conn.Table<Wallet>().ToListAsync();
            return list;
        }


        public async Task<Wallet> GetById(int id)
        {
            await Init();
            var wallet = await conn.Table<Wallet>().FirstOrDefaultAsync(x => x.Id == id);
            return wallet;
        }

        public async Task<Wallet> GetFirstWallet()
        {
            await Init();
            var wallet = await conn.Table<Wallet>().FirstOrDefaultAsync();
            return wallet;
        }

        public async Task<int> UpdateWallet(Wallet wallet)
        {
            await Init();
            int result = await conn.UpdateAsync(wallet);
            return result;
        }

        public async Task<int> DeleteWallet(int id)
        {
            await Init();
            int result = await conn.DeleteAsync<Wallet>(id);
            return result;
        }
    }
}

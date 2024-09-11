using DotnetMauiApp.Data;
using DotnetMauiApp.Models;
using Microsoft.EntityFrameworkCore;
using SQLite;

namespace DotnetMauiApp.Repositories
{
    public class WalletRepository
    {
        DataContext _context;

        public WalletRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<IEnumerable<Wallet>> GetAllWallet()
        {
            var wallets = await _context.Wallets.ToListAsync();
            return wallets;
        }

        public async Task<Wallet?> GetWalletById(int walletId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == walletId);
            return wallet;
        }

        public async Task<Wallet?> GetFirstWallet()
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync();
            return wallet;
        }
        public async Task<int> AddWallet(Wallet wallet)
        {
            var created_wallet = _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return created_wallet.Entity.Id;
        }

        public async Task UpdateWallet(Wallet wallet, Wallet updateWallet)
        {
            _context.Entry(wallet).CurrentValues.SetValues(updateWallet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWallet(Wallet wallet)
        {
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
        }
    }
}

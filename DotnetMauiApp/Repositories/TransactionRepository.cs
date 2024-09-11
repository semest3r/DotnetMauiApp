using DotnetMauiApp.Data;
using DotnetMauiApp.Models;
using Microsoft.EntityFrameworkCore;


namespace DotnetMauiApp.Repositories
{
    public class TransactionRepository
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext dataContext)
        {
            _context= dataContext;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransaction(int walletId, DateTime dateFrom, DateTime dateTo)
        {
            var transactions = await _context.Transactions
                .Where(x => x.WalletId == walletId && x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetRecentTransaction(int WalletId)
        {
            var transactions = await _context.Transactions
                .Take(10)
                .Where(x=> x.WalletId == WalletId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return transactions;
        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            var transaksi = await _context.Transactions
                .FirstOrDefaultAsync(x => x.Id == id);
            return transaksi;
        }

        public double GetTotalDespoit(int walletId, DateTime dateFrom, DateTime dateTo)
        {
            var totalDeposit = _context.Transactions
                .Where(x => x.WalletId == walletId && x.TypeTransaction == "In" && x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                .Sum(x => x.TotalMoney);
            return totalDeposit;
        }

        public double GetTotalWithdraw(int walletId, DateTime dateFrom, DateTime dateTo)
        {
            var totalWithdraw = _context.Transactions
                .Where(x => x.WalletId == walletId && x.TypeTransaction == "Out" && x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                .Sum(x => x.TotalMoney);
            return totalWithdraw;
        }

        public async Task<int> AddTransaction(Transaction transaction)
        {
            var result = _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}

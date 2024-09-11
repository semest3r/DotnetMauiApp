
using SQLite;

namespace DotnetMauiApp.Models
{
    [Table("transactions")]
    public partial class Transaction
    {
        [PrimaryKey,  AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public double TotalMoney { get; set; }
        public double CurrrentTotalMoney { get; set; }
        public string TypeTransaction { get; set; }
        public DateTime CreatedAt { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
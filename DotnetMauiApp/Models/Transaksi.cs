
using SQLite;

namespace DotnetMauiApp.Models
{
    [Table("transaksi")]
    public partial class Transaksi
    {
        [PrimaryKey,  AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public double JumlahUang { get; set; }
        public double TotalUang { get; set; }
        public string TipeTransaksi { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
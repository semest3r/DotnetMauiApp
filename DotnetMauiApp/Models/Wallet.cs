using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetMauiApp.Models
{
    [Table("wallet")]
    public class Wallet
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalUang { get; set; }
        public bool IsActive { get; set; }
    }
}

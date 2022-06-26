using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Transaction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; } 
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }
        public int ToAccountId { get; set; }
        public decimal ToAccountBalance { get; set; }
        public DateTime TransactionTime { get; set; }

        //public ICollection<Account> Accounts { get; set; }
    }
}
